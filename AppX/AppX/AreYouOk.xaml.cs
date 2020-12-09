using AppX.DatabaseClasses;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AreYouOk : ContentPage
    {
        public AreYouOk()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MainPage.noAnwserAfterFall = false;
        }

        public async void EverythingOk(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PopAsync();   //If everything is ok then goes back to main page
        }

        public async void NotFine(object sender, EventArgs args)        //If user clicks NO then the text is sent to emergency contacts
        {
            ObservableCollection<ContactsDB> contactsList;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<ContactsDB>();
                var contacts = conn.Table<ContactsDB>().ToList();

                contactsList = new ObservableCollection<ContactsDB>(contacts);
            }

            bool smsSent=true;
            foreach (var contact in contactsList)
            {
                try
                {                                                                                                                //Try-catch in case the user won't allow acces to Text messages
                    SendTextAndEmail s = new SendTextAndEmail();
                    smsSent = s.Send("Upadek! Sprawdź czy wszystko w porządku z twoim podopiecznym!", contact.PhoneNumber);
                }
                catch (Exception ex)
                {
                }
            }

            if(smsSent)
            {
                await DisplayAlert("SMS został wysłany!", "Czekaj na kontakt od opiekuna!", "OK");
            }
            else
            {
                await DisplayAlert("Brak dostępu do wiadomości!", "Zezwól na dostęp do wiadomości", "OK");
            }

            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}