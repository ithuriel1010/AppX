using AppX.Contacts;
using AppX.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}