using AppX.DatabaseClasses;
using AppX.Utils;
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
    public class EditLocalizationViewModel : INotifyPropertyChanged
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

        private string errorMessage { get; set; }
        private bool correctStreet { get; set; }
        private bool correctHouseNumber { get; set; }
        private bool correctCity { get; set; }
        private bool correctCounty { get; set; }
        private bool correctMessage { get; set; }
        private bool correctName { get; set; }

        private Color nameTextColor = Color.Red;
        private Color messageTextColor = Color.Red;
        private Color streetTextColor = Color.Red;
        private Color houseNumberTextColor = Color.Red;
        private Color cityTextColor = Color.Red;
        private Color countyTextColor = Color.Red;

        public Color NameTextColor
        {
            get => nameTextColor;
            set
            {
                nameTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(NameTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public Color MessageTextColor
        {
            get => messageTextColor;
            set
            {
                messageTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(MessageTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public Color StreetTextColor
        {
            get => streetTextColor;
            set
            {
                streetTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(StreetTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public Color HouseNumberTextColor
        {
            get => houseNumberTextColor;
            set
            {
                houseNumberTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(HouseNumberTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public Color CityTextColor
        {
            get => cityTextColor;
            set
            {
                cityTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(CityTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public Color CountyTextColor
        {
            get => countyTextColor;
            set
            {
                countyTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(CountyTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }

        double lat;
        double lon;
        public ICommand GetPositionCommand { get; }
        public ICommand CancelCommand { get; }
        public Command SaveCommand { get; }

        LocalizationsDB localization;

        public EditLocalizationViewModel(LocalizationsDB localizationDetails)
        {
            localization = localizationDetails;

            //fullAddress = localization.Address;
            Street = localization.Street;
            HouseNumber = localization.HouseNumber;
            City = localization.City;
            County = localization.County;
            //lat = localization.Lat;
            //lon = localization.Lon;
            Name = localization.Name;
            Message = localization.Message;

            SaveCommand = new Command(async () =>
            {
                if (correctName && correctMessage && correctStreet && correctHouseNumber && correctCity && correctCounty)
                {
                    fullAddress = Street + " " + HouseNumber + " " + City + " " + County + " Polska";
                    await OnGetPosition(fullAddress);

                    localization.Street = Street;
                    localization.HouseNumber = HouseNumber;
                    localization.City = City;
                    localization.County = County;
                    localization.Address = fullAddress;
                    localization.Lat = lat;
                    localization.Lon = lon;
                    localization.Name = Name;
                    localization.Message = Message;


                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<LocalizationsDB>();
                        conn.Update(localization);
                    }

                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
                else
                {
                    ErrorMessage = "Co najmniej jedno z pól jest nieprawidłowo wypełnione";
                }


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
                (StreetTextColor, correctStreet) = RegexUtill.Check(RegexUtill.MinLength(3), value);
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
                (HouseNumberTextColor, correctHouseNumber) = RegexUtill.Check(RegexUtill.MinLengthNumbers(1), value);
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
                (CityTextColor, correctCity) = RegexUtill.Check(RegexUtill.MinLength(3), value);
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
                (CountyTextColor, correctCounty) = RegexUtill.Check(RegexUtill.MinLength(3), value);
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
                (NameTextColor, correctName) = RegexUtill.Check(RegexUtill.MinLength(3), value);
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
                (MessageTextColor, correctMessage) = RegexUtill.Check(RegexUtill.MinLength(3), value);
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
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                var args = new PropertyChangedEventArgs(nameof(ErrorMessage));

                PropertyChanged?.Invoke(this, args);
            }
        }

    }
}
