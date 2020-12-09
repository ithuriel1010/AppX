using Android.Icu.Text;
using AppX.DatabaseClasses;
using AppX.LocalizationFiles;
using AppX.Settings;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Xml.Schema;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppX
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public string Lokalizacja { get; set; }
        public string Acc { get; set; }
        SensorSpeed speed = SensorSpeed.UI;

        double currentLongitude;
        double currentLatitude;
        int farAwayMinutes = 0;
        int minutesClose = 0;
        int minutesWithoutLocalization = 0;

        public static bool noAnwserAfterFall = false;
        private int ignoreFallNotificationSeconds = 0;
        public static bool refreshPage { get; set; }

        List<LocalizationsDB> localizationsList;
        PatientDB patient;

        public MainPage()
        {
            InitializeComponent();

            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<PatientDB>();
                patient = conn.Table<PatientDB>().FirstOrDefault();
            }

            System.Timers.Timer timer = new System.Timers.Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler( CheckLocation);         //Location is checked every minute
            timer.Start();

            if (Accelerometer.IsMonitoring)     
                return;

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;       //Reacts to any accelerometer changes
            Accelerometer.Start(SensorSpeed.UI);          
        }

        private void SendNotification(string title, string message, string action)
        {
            DependencyService.Get<INotification>().CreateNotification(title, message, action);
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            double readingX = data.Acceleration.X;
            double readingY = data.Acceleration.Y;
            double readingZ = data.Acceleration.Z;

            double accelerationMagnitude = Math.Sqrt(Math.Pow(readingX, 2)
                    + Math.Pow(readingY, 2)
                    + Math.Pow(readingZ, 2));

            DecimalFormat precision = new DecimalFormat("0.00");
            double roundedMagnitude = double.Parse(precision.Format(accelerationMagnitude));

            if (roundedMagnitude > 0.3d && roundedMagnitude < 0.4d)
            {
                Accelerometer.Stop();       //Accelerometer is stopped after fall detection so that the fall is only detected once. Without that one fall would be counted as multiple falls
                DisplayFallDetection();
                noAnwserAfterFall = true;   //Is true untill user clicks in the notification about the fall
                StartTimer();
            }
        }
        public async void StartTimer()      //Counts how many seconds user ignores the notification about fall
        {
            ignoreFallNotificationSeconds = 0;

            while (noAnwserAfterFall)
            {
                await Task.Delay(1000);
                ignoreFallNotificationSeconds++;

                if (Accelerometer.IsMonitoring == false)
                {
                    Accelerometer.Start(speed);
                }

                if (ignoreFallNotificationSeconds>=patient.FallSeconds && noAnwserAfterFall)    //When user doesn't click on the notification for the time in settings
                {
                    ObservableCollection<ContactsDB> contactsList;
                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<ContactsDB>();
                        var contacts = conn.Table<ContactsDB>().ToList();

                        contactsList = new ObservableCollection<ContactsDB>(contacts);
                    }

                    foreach(var contact in contactsList)
                    {
                        SendTextAndEmail s = new SendTextAndEmail();
                        s.Send("Upadek! Sprawdź czy wszystko w porządku z twoim podopiecznym!", contact.PhoneNumber); 
                    }

                    ignoreFallNotificationSeconds = 0;
                }

            }
        }

        public async void ClickedFallNotification()
        {
            noAnwserAfterFall = false;
        }
        public void DisplayFallDetection()
        {
            SendNotification("Uwaga! Wykryto Upadek!", "Wszystko w porządku?", "FallAlert");
        }
        public void CheckLocation(object sender, ElapsedEventArgs e)        //Method that is called every minute
        {
            try
            {                                                   //Try-catch is necessary if user doesn't allow to check localization
                GetLocationAsync();
                CheckDistanceAndSendAlert();
            }
            catch(Exception ex)
            {
                minutesWithoutLocalization++;
                if(minutesWithoutLocalization>=5)
                {
                    SendNotification("Brak dostępu do lokalizacji!", "Zezwól na dostęp do lokalizacji aby aplikacja działała lepiej", "LocalizationAlert");
                    minutesWithoutLocalization = 0;
                }
            }
        }
        public static async void ShouldRefreshPage(bool refresh)
        {

            refreshPage = refresh;

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();            

            if (refreshPage)
            {
                var vUpdatedPage = new MainPage();
                Application.Current.MainPage.Navigation.InsertPageBefore(vUpdatedPage, this);
                await Application.Current.MainPage.Navigation.PopAsync();
                ShouldRefreshPage(false);
            }
        }

        public async void AddButtonClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Dodaj", "Wstecz", null, "Kontakt Awaryjny", "Osobę bliską", "Lokalizację");

            if (action == "Kontakt Awaryjny")
            {
                var addContVM = new AddContactViewModel();
                var addContPage = new AddContact();

                addContPage.BindingContext = addContVM;
                await Application.Current.MainPage.Navigation.PushAsync(addContPage);
            }
            else if (action == "Lokalizację")
            {
                var addLocVM = new AddLocalizationViewModel();
                var addLocPage = new AddLocalization();

                addLocPage.BindingContext = addLocVM;
                await Application.Current.MainPage.Navigation.PushAsync(addLocPage);
            }
            else if (action == "Osobę bliską")
            {
                var addPerVM = new AddPersonViewModel();
                var addPerPage = new AddPerson();

                addPerPage.BindingContext = addPerVM;
                await Application.Current.MainPage.Navigation.PushAsync(addPerPage);
            }

        }

        public async void SeeButtonClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Przeglądaj", "Wstecz", null, "Kontakty awaryjne", "Osoby bliskie", "Lokalizację");

            if (action == "Kontakty awaryjne")
            {
                var seeContVM = new SeeContactsListViewModel();
                var seeContPage = new SeeContactsList();

                seeContPage.BindingContext = seeContVM;
                await Application.Current.MainPage.Navigation.PushAsync(seeContPage);
            }
            if (action == "Lokalizację")
            {
                var seelocalVM = new SeeLocalizationsListViewModel();
                var seelocalPage = new SeeLocalizationsList();

                seelocalPage.BindingContext = seelocalVM;
                await Application.Current.MainPage.Navigation.PushAsync(seelocalPage);
            }
            if (action == "Osoby bliskie")
            {
                var personVM = new SeePersonsListViewModel();
                var personPage = new SeePersonsList();

                personPage.BindingContext = personVM;
                await Application.Current.MainPage.Navigation.PushAsync(personPage);
            }
        }

        public async void SettingsButtonClicked(object sender, EventArgs e)
        {
            var settingsVM = new SettingsPageViewModel();
            var settingsPage = new SettingsPage();

            settingsPage.BindingContext = settingsVM;
            await Application.Current.MainPage.Navigation.PushAsync(settingsPage);
        }

        void GetLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = Geolocation.GetLocationAsync(request).Result;

                if (location != null)
                {
                    currentLatitude = location.Latitude;
                    currentLongitude = location.Longitude;
                }
                else
                {
                    currentLatitude = 0;
                    currentLongitude = 0;
                }
            }            
            catch (Exception ex)
            {
            }
        }

        public async void CheckDistanceAndSendAlert()
        {
            using (SQLiteConnection loc = new SQLiteConnection(App.FilePath))
            {
                loc.CreateTable<LocalizationsDB>();
                var localizations = loc.Table<LocalizationsDB>().ToList();

                localizationsList = new List<LocalizationsDB>(localizations);
            }

            int locationCount = localizationsList.Count;
            int farLocations = 0;

            foreach (var oneLocalization in localizationsList)
            {
                Location other = new Location(oneLocalization.Lat, oneLocalization.Lon);
                Location here = new Location(currentLatitude, currentLongitude);
                double kilometers = Location.CalculateDistance(other, here, DistanceUnits.Kilometers);

                if (kilometers <= 0.05)
                {
                    minutesClose++;

                    if (minutesClose >= 5)      //If user is in a known location for 5 minutes send a notification
                    {
                        SendNotification(oneLocalization.Name, oneLocalization.Message, "LocalizationAlert");
                        minutesClose = 0;
                        break;
                    }

                }
                else if (kilometers >= 10)
                {
                    farLocations++;

                    if(farLocations==locationCount)     //If user is far away from ALL saved locations
                    {
                        farAwayMinutes++;

                        if(farAwayMinutes>=patient.LocalizationMinutes)     //If user is far away from all known locations for a time set in settings sendnotification and text to contacts
                        {
                            bool sent;
                            SendNotification("Uwaga", "Jesteś daleko od znanych lokalizacji. Wiadomość została wysłana do twojego opiekuna", "LocalizationAlert");

                            ObservableCollection<ContactsDB> contactsList;
                            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                            {
                                conn.CreateTable<ContactsDB>();
                                var contacts = conn.Table<ContactsDB>().ToList();

                                contactsList = new ObservableCollection<ContactsDB>(contacts);

                            }

                            foreach (var contact in contactsList)
                            {
                                try
                                {
                                    SendTextAndEmail sa = new SendTextAndEmail();
                                    sa.Send("Twój podopieczny jest daleko od znanych lokalizacji!", contact.PhoneNumber);
                                    SendTextAndEmail s = new SendTextAndEmail();
                                }
                                catch (Exception ex)
                                {
                                }
                                
                            }

                            farAwayMinutes = 0;
                        }
                    }
                }
            }
        }
    }
}
