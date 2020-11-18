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
            //mp.ClickedNotification();
        }

        public async void EverythingOk(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async void NotFine(object sender, EventArgs args)
        {
            ObservableCollection<ContactsDB> contactsList;
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<ContactsDB>();
                var contacts = conn.Table<ContactsDB>().ToList();

                contactsList = new ObservableCollection<ContactsDB>(contacts);
            }

            foreach (var contact in contactsList)
            {
                SendTextAndEmail s = new SendTextAndEmail("Upadek! Sprawdź czy wszystko w porządku z twoim podopiecznym!", contact.PhoneNumber, "ithuriel1010@gmail.com");
            }

            await DisplayAlert("SMS został wysłany!", "Czekaj na kontakt od opiekuna!", "OK");

            await Application.Current.MainPage.Navigation.PopAsync();

            //SendTextAndEmail s = new SendTextAndEmail("Upadek", "+48604051870", "ithuriel1010@gmail.com");
        }
    }
}