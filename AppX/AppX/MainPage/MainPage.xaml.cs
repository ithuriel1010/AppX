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

        List<LocalizationsDB> localizationsList;

        public MainPage()
        {
            InitializeComponent();

            System.Timers.Timer timer = new System.Timers.Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(MyMethod);
            timer.Start();

            //GetLocationAsync();
            //AccelerometerTest at = new AccelerometerTest(Acc);
            //at.ToggleAccelerometer();
            //var lok = new Label { Text = Lokalizacja, TextDecorations = TextDecorations.Underline};

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

            foreach (var oneLocalization in localizationsList)
            {
                Location other = new Location(oneLocalization.Lat, oneLocalization.Lon);
                Location here = new Location(latitude, longitude);
                double kilometers = Location.CalculateDistance(other, here, DistanceUnits.Kilometers);

                if (kilometers <= 0.05 || true)
                {
                    Device.BeginInvokeOnMainThread(async () => {
                        await DisplayAlert(oneLocalization.Name, kilometers.ToString(), "OK");
                    });
                    //await DisplayAlert(oneLocalization.Name, oneLocalization.Message, "OK");
                    break;
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
