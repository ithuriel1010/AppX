using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppX.LocalizationFiles
{
    public class AddLocalizationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public AddLocalizationViewModel()
        {
            SaveCommand = new Command(async () =>
            {

            });

            CancelCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();

            });
        }

        //string address;
        string placeLocalization;

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

        public string PlaceLocalization
        {
            get => placeLocalization;
            set
            {
                placeLocalization = value;
                var args = new PropertyChangedEventArgs(nameof(PlaceLocalization));

                PropertyChanged?.Invoke(this, args);
            }
        }

    }
}
