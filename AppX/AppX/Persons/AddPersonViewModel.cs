using AppX.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace AppX
{
    public class AddPersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        PersonsDB person = new PersonsDB();
        public string photo = "smile";
        private AddPerson p = new AddPerson();
        public AddPersonViewModel()
        {
            SaveCommand = new Command(async () =>
            {
                person.Imie = Imie;
                person.Nazwisko = Nazwisko;
                person.Telefon = Telefon;
                person.DataUrodzenia = DataUrodzenia;
                person.Zwiazek = Zwiazek;
                person.Zdjecie = photo;                

                using(SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<PersonsDB>();
                    conn.Insert(person);
                }

                await Application.Current.MainPage.Navigation.PopAsync();

            });

            PhotoCommand = new Command(async () =>
            {
                photo = await p.UploadPhoto();
            });

            ShowPhoto = new Command( () =>
            {
                p.test(photo);
            });

            CancelCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();

            });
        }

        string imie;
        string nazwisko;
        string telefon;
        DateTime dataUrodzenia;
        string zwiazek;

        public string MaxDate = DateTime.Now.ToString("MM/dd/yyyy");
        //public long MaxDate = (long) (DateTime.Now - new DateTime(1900, 1, 1)).TotalMilliseconds;


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command PhotoCommand { get; }
        public Command ShowPhoto { get; }



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
        public DateTime DataUrodzenia
        {
            get => dataUrodzenia;
            set
            {
                dataUrodzenia = value;
                var args = new PropertyChangedEventArgs(nameof(DataUrodzenia));

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

