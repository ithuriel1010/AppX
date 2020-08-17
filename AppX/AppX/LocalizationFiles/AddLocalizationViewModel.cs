using AppX.DatabaseClasses;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppX.LocalizationFiles
{
    public class AddLocalizationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        bool IsBusy;
        //string address = "Microsoft Building 25 Redmond WA USA";
        //string geocodePosition;
        string street;
        string houseNumber;
        string city;
        string county;
        string fullAddress;
        string name;
        string message;

        double lat;
        double lon;
        public ICommand GetPositionCommand { get; }
        public ICommand CancelCommand { get; }
        public Command SaveCommand { get; }

        LocalizationsDB localization = new LocalizationsDB();


        public AddLocalizationViewModel()
        {
            SaveCommand = new Command(async () =>
            {
                fullAddress = Street + " " + HouseNumber + " " + City + " " + County + " Polska";
                await OnGetPosition(fullAddress);

                localization.Address = fullAddress;
                localization.Lat = lat;
                localization.Lon = lon;
                localization.Name = Name;
                localization.Message = Message;

                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<LocalizationsDB>();
                    conn.Insert(localization);
                }

                await Application.Current.MainPage.Navigation.PopAsync();

            });

            GetPositionCommand = new Command(async () =>
            {
                fullAddress = Street + " " + HouseNumber + " " + City + " " + County + " Polska";
                await OnGetPosition(fullAddress);
            });

            CancelCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();

            });
        }

        /*public string Address
        {
            get => address;
            set
            {
                address = value;
                var args = new PropertyChangedEventArgs(nameof(Address));

                PropertyChanged?.Invoke(this, args);
            }
        }*/

        public string Street
        {
            get => street;
            set
            {
                street = value;
                var args = new PropertyChangedEventArgs(nameof(Street));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public string HouseNumber
        {
            get => houseNumber;
            set
            {
                houseNumber = value;
                var args = new PropertyChangedEventArgs(nameof(HouseNumber));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public string City
        {
            get => city;
            set
            {
                city = value;
                var args = new PropertyChangedEventArgs(nameof(City));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public string County
        {
            get => county;
            set
            {
                county = value;
                var args = new PropertyChangedEventArgs(nameof(County));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                var args = new PropertyChangedEventArgs(nameof(Name));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public string Message
        {
            get => message;
            set
            {
                message = value;
                var args = new PropertyChangedEventArgs(nameof(Message));

                PropertyChanged?.Invoke(this, args);
            }
        }

        /*public string GeocodePosition
        {
            get => geocodePosition;
            set
            {
                geocodePosition = value;
                var args = new PropertyChangedEventArgs(nameof(GeocodePosition));

                PropertyChanged?.Invoke(this, args);
            }
        }*/

        async Task OnGetPosition(string fullAddress)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {

                var locations = await Geocoding.GetLocationsAsync(fullAddress);
                Location location = locations.FirstOrDefault();
                if (location == null)
                {
                    //GeocodePosition = "Unable to detect locations";
                }
                else
                {
                    lat = location.Latitude;
                    lon = location.Longitude;

                    /*GeocodePosition =
                        $"{nameof(location.Latitude)}: {location.Latitude}\n" +
                        $"{nameof(location.Longitude)}: {location.Longitude}\n";*/
                }
            }
            catch (Exception ex)
            {
                //GeocodePosition = $"Unable to detect locations: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
