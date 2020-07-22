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
        string address = "Microsoft Building 25 Redmond WA USA";
        string geocodePosition;

        public ICommand GetPositionCommand { get; }
        public ICommand CancelCommand { get; }


        public AddLocalizationViewModel()
        {
            GetPositionCommand = new Command(async () => await OnGetPosition());


            CancelCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();

            });
        }

        public string Address
        {
            get => address;
            set
            {
                address = value;
                var args = new PropertyChangedEventArgs(nameof(Address));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public string GeocodePosition
        {
            get => geocodePosition;
            set
            {
                geocodePosition = value;
                var args = new PropertyChangedEventArgs(nameof(GeocodePosition));

                PropertyChanged?.Invoke(this, args);
            }
        }

        async Task OnGetPosition()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {

                var locations = await Geocoding.GetLocationsAsync(Address);
                Location location = locations.FirstOrDefault();
                if (location == null)
                {
                    GeocodePosition = "Unable to detect locations";
                }
                else
                {
                    GeocodePosition =
                        $"{nameof(location.Latitude)}: {location.Latitude}\n" +
                        $"{nameof(location.Longitude)}: {location.Longitude}\n";
                }
            }
            catch (Exception ex)
            {
                GeocodePosition = $"Unable to detect locations: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
