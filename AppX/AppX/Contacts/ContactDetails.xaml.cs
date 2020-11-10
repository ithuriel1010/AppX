using AppX.Contacts;
using AppX.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDetails : ContentPage
    {
        public static ContactsDB contact;
        public ContactDetails()
        {
            InitializeComponent();
        }
        public async void Edit(ContactsDB contactDetails)
        {
            contact = contactDetails;
            var editContactVM = new EditContactViewModel(contact);
            var esitContactPage = new EditContact();

            esitContactPage.BindingContext = editContactVM;
            await Application.Current.MainPage.Navigation.PushAsync(esitContactPage);
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