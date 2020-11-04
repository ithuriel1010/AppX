using AppX.DatabaseClasses;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AppX
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private PatientDB patient;
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string fullName { get; set; }
        public string telefon { get; set; }
        public DateTime dataUrodzenia { get; set; }
        public string hobby { get; set; }
        public string zdjecie { get; set; }
        public int wiek { get; set; }
        public MainPageViewModel()
        {
            using (SQLiteConnection pat = new SQLiteConnection(App.FilePath))
            {
                pat.CreateTable<PatientDB>();
                patient = pat.Table<PatientDB>().FirstOrDefault();
            }

            imie = patient.Imie;
            nazwisko = patient.Nazwisko;
            fullName = imie + " " + nazwisko;
            telefon = patient.Telefon;
            dataUrodzenia = patient.DataUrodzenia;
            hobby = patient.Hobby;
            zdjecie = patient.Zdjecie;
            wiek = DateTime.Now.Year - patient.DataUrodzenia.Year;

        }

    }
}
