using AppX.LocalizationFiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MainPage()
        {
            InitializeComponent();
            GetLocationAsync();
            //AccelerometerTest at = new AccelerometerTest(Acc);
            //at.ToggleAccelerometer();
            //var lok = new Label { Text = Lokalizacja, TextDecorations = TextDecorations.Underline};

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
                var localVM = new LocalizationViewModel(Lokalizacja);
                var localPage = new Localization();

                localPage.BindingContext = localVM;
                await Application.Current.MainPage.Navigation.PushAsync(localPage);
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
                    Lokalizacja = $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
                else
                {
                    Lokalizacja = "Lokalizacja=null";
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
