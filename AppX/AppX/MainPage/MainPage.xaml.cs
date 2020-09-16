using Android.Icu.Text;
using AppX.DatabaseClasses;
using AppX.LocalizationFiles;
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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppX
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public string Lokalizacja { get; set; }
        public string Acc { get; set; }
        SensorSpeed speed = SensorSpeed.UI;

        double longitude;
        double latitude;
        int minutesAway = 0;

        bool noAnwser = false;
        private int _duration = 0;

        List<LocalizationsDB> localizationsList;

        public MainPage()
        {
            InitializeComponent();

            System.Timers.Timer timer = new System.Timers.Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(MyMethod);
            timer.Start();

            if (Accelerometer.IsMonitoring)
                return;

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.UI);
            

            /*if (Gyroscope.IsMonitoring)
                return;

            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
            Gyroscope.Start(SensorSpeed.UI);*/

            //GetLocationAsync();
            //AccelerometerTest at = new AccelerometerTest(Acc);
            //at.ToggleAccelerometer();
            //var lok = new Label { Text = Lokalizacja, TextDecorations = TextDecorations.Underline};

        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            //XA.Text = "X:" + data.Acceleration.X.ToString();
            //YA.Text = "Y:" + data.Acceleration.Y.ToString();
            //ZA.Text = "Z:" + data.Acceleration.Z.ToString();

            double loX = data.Acceleration.X;
            double loY = data.Acceleration.Y;
            double loZ = data.Acceleration.Z;

            double loAccelerationReader = Math.Sqrt(Math.Pow(loX, 2)
                    + Math.Pow(loY, 2)
                    + Math.Pow(loZ, 2));

            DecimalFormat precision = new DecimalFormat("0.00");
            double ldAccRound = double.Parse(precision.Format(loAccelerationReader));

            if (ldAccRound > 0.45d && ldAccRound < 0.5d)
            {
                Accelerometer.Stop();
                DisplayFallDetection();
                noAnwser = true;
                StartTimer();

            }

            // Process Acceleration X, Y, and Z
        }
        public async void StartTimer()
        {
            _duration = 0;

            // tick every second while game is in progress
            while (noAnwser)
            {
                await Task.Delay(1000);
                _duration++;

                if(_duration>=60)
                {
                    await DisplayAlert("UWAGA", "Wykryto Upadek!", "OK");
                    _duration = 0;

                }

            }
        }

        /*void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            var data = e.Reading;
            XG.Text = "X:" + data.AngularVelocity.X.ToString();
            YG.Text = "Y:" + data.AngularVelocity.Y.ToString();
            ZG.Text = "Z:" + data.AngularVelocity.Z.ToString();
        }*/

        public async void DisplayFallDetection()
        {
            var answer = await DisplayActionSheet("WYKRYTO UPADEK!!!", "Nie", "Tak", "Czy wszystko w porządku?");

            if (answer == "Tak")
            {
                //Accelerometer.Start(SensorSpeed.UI);
            }
            else if (answer=="Nie") 
            {
                await DisplayAlert("UWAGA", "Wykryto Upadek!", "OK");
                //Accelerometer.Start(SensorSpeed.UI);

            }

            noAnwser = false;
            Accelerometer.Start(SensorSpeed.UI);

        }

        public void MyMethod(object sender, ElapsedEventArgs e)
        {
            GetLocationAsync();
            CheckDistanceAndSendAlert();
            //LocalizationLabel.Text = Lokalizacja;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //NotesList.SelectedItem = null;
        }

        public async void ClickAction(object sender, EventArgs e)
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

        public async void SeeClicked(object sender, EventArgs e)
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

        public async void AddNote(object sender, EventArgs e)
        {
            var addNoteVM = new AddNoteViewModel();
            var addNotePage = new AddNote();

            addNotePage.BindingContext = addNoteVM;
            await Application.Current.MainPage.Navigation.PushAsync(addNotePage);
        }

        async void GetLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    latitude = location.Latitude;
                    longitude = location.Longitude;

                    //Lokalizacja = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
                else
                {
                    latitude = 0;
                    longitude = 0;
                    //Lokalizacja = "Lokalizacja=null";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Lokalizacja = "Handle not supported on device exception";
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Lokalizacja = "Handle not enabled on device exception";
            }
            catch (PermissionException pEx)
            {
                Lokalizacja = "Handle permission exception";
            }
            catch (Exception ex)
            {
                Lokalizacja = "Unable to get location";
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
                Location here = new Location(latitude, longitude);
                double kilometers = Location.CalculateDistance(other, here, DistanceUnits.Kilometers);

                if (kilometers <= 0.05)
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await DisplayAlert(oneLocalization.Name, oneLocalization.Message, "OK");
                    });
                    //await DisplayAlert(oneLocalization.Name, oneLocalization.Message, "OK");
                    break;
                }
                else if (kilometers >= 3)
                {
                    farLocations++;

                    if(farLocations==locationCount)
                    {
                        minutesAway++;

                        if(minutesAway>=10)
                        {
                            Device.BeginInvokeOnMainThread(async () => {
                                await DisplayAlert("UWAGA", "Jesteś daleko poza znanymi lokalizacjami!", "OK");
                            });

                            minutesAway = 0;
                        }
                    }
                }
            }
        }
    }

    //public class AccelerometerTest
    //{
    //    // Set speed delay for monitoring changes.
    //    SensorSpeed speed = SensorSpeed.UI;
    //    public string Acc { get; set; }

    //    public AccelerometerTest(string Acc)
    //    {
    //        // Register for reading changes, be sure to unsubscribe when finished
    //        Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
    //        this.Acc = Acc;
    //    }

    //    void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
    //    {
    //        var data = e.Reading;
    //        Acc = $"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}";
    //        // Process Acceleration X, Y, and Z
    //    }

    //    public void ToggleAccelerometer()
    //    {
    //        try
    //        {
    //            if (Accelerometer.IsMonitoring)
    //                Accelerometer.Stop();
    //            else
    //                Accelerometer.Start(speed);
    //        }
    //        catch (FeatureNotSupportedException fnsEx)
    //        {
    //            // Feature not supported on device
    //        }
    //        catch (Exception ex)
    //        {
    //            // Other error has occurred.
    //        }
    //    }
    //}

}
