using AppX.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppX.Persons
{
    public class PersonDetailsViewModel
    {
        public PersonsDB person;

        public string imie;
        public string nazwisko;
        public string fullName { get; set; }
        public string telefon { get; set; }
        public DateTime dataUrodzenia { get; set; }
        public string zwiazek { get; set; }
        public string zdjecie { get; set; }
        public int wiek { get; set; }

        public PersonDetailsViewModel(PersonsDB person)
        {
            this.person = person;
            imie = person.Imie;
            nazwisko = person.Nazwisko;
            fullName = imie + " " + nazwisko;
            telefon = person.Telefon;
            dataUrodzenia = person.DataUrodzenia;
            zwiazek = person.Zwiazek;
            zdjecie = person.Zdjecie;
            wiek = person.Wiek;
        }
    }
}
