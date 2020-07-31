using Android.Content.Res;
using AppX.DatabaseClasses;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AppX
{
    public class AddPatientInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        PatientDB patient = new PatientDB();
        public string photo = "smile";
        private AddPerson p = new AddPerson();
        private AddPatientInfo ap = new AddPatientInfo();

        string imie;
        string nazwisko;
        string telefon;
        DateTime dataUrodzenia;
        string hobby;
        App app;


        public string MaxDate = DateTime.Now.ToString("MM/dd/yyyy");
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command PhotoCommand { get; }

        public AddPatientInfoViewModel(App app)
        {
            this.app = app;
            SaveCommand = new Command(async () =>
            {
                patient.HaveData = true;
                patient.Imie = Imie;
                patient.Nazwisko = Nazwisko;
                patient.Telefon = Telefon;
                patient.DataUrodzenia = DataUrodzenia;
                patient.Hobby = Hobby;
                patient.Zdjecie = photo;

                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<PatientDB>();
                    conn.Insert(patient);
                }

                app.SetHomePage();

            });

            PhotoCommand = new Command(async () =>
            {
                photo = await ap.UploadPhoto();
            });

        }

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
        public string Hobby
        {
            get => hobby;
            set
            {
                hobby = value;
                var args = new PropertyChangedEventArgs(nameof(Hobby));

                PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
