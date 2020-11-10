using AppX.Classes;
using AppX.LocalizationFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppX.Persons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonDetails : ContentPage
    {
        public static PersonsDB person;
        public PersonDetails()
        {
            InitializeComponent();
        }
        public async void Edit(PersonsDB personDetails)
        {
            person = personDetails;
            var editPersonVM = new EditPersonViewModel(person);
            var esitPersonPage = new EditPerson();

            esitPersonPage.BindingContext = editPersonVM;
            await Application.Current.MainPage.Navigation.PushAsync(esitPersonPage);
        }

        public void PlacePhoneCall(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}