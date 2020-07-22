using Android.Provider;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using SQLite;
using AppX.DatabaseClasses;

namespace AppX
{
    public class AddContactViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ContactsDB contact = new ContactsDB();
        public AddContactViewModel()
        {
            SaveCommand = new Command(async () =>
             {
                 contact.Imie = Imie;
                 contact.Nazwisko = Nazwisko;
                 contact.Telefon = Telefon;
                 contact.Email = Email;
                 contact.Zwiazek = Zwiazek;

                 using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                 {
                     conn.CreateTable<ContactsDB>();
                     conn.Insert(contact);
                 }

                 await Application.Current.MainPage.Navigation.PopAsync();

             });

            CancelCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();

            });
        }
        

        string imie;
        string nazwisko;
        string telefon;
        string email;
        string zwiazek;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        public string Imie
        {
            get => imie;
            set
            {
                imie = value;
                var args = new PropertyChangedEventArgs(nameof(Imie));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public string Nazwisko
        {
            get => nazwisko;
            set
            {
                nazwisko = value;
                var args = new PropertyChangedEventArgs(nameof(Nazwisko));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public string Telefon
        {
            get => telefon;
            set
            {
                telefon = value;
                var args = new PropertyChangedEventArgs(nameof(Telefon));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                var args = new PropertyChangedEventArgs(nameof(Email));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public string Zwiazek
        {
            get => zwiazek;
            set
            {
                zwiazek = value;
                var args = new PropertyChangedEventArgs(nameof(Zwiazek));

                PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
