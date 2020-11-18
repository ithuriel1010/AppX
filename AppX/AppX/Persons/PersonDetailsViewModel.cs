using AppX.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppX.Persons
{
    public class PersonDetailsViewModel
    {
        public PersonsDB person;

        public string imie { get; set; }
        public string nazwisko { get; set; }
        public string fullName { get; set; }
        public string telefon { get; set; }
        public DateTime dataUrodzenia { get; set; }
        public string zwiazek { get; set; }
        public string zdjecie { get; set; }
        public int wiek { get; set; }

        public Command EditCommand { get; }
        public Command QuickCall { get; }
        private PersonDetails p = new PersonDetails();


        public PersonDetailsViewModel(PersonsDB person)
        {           
            this.person = person;
            imie = person.FirstName;
            nazwisko = person.LastName;
            fullName = imie + " " + nazwisko;
            telefon = person.PhoneNumber;
            dataUrodzenia = person.BirthDate;
            zwiazek = person.Relationship;
            zdjecie = person.Photo;
            wiek = person.Age;

            EditCommand = new Command(async () =>
            {
                p.Edit(person);

            });

            QuickCall = new Command(() =>
            {
                p.PlacePhoneCall(telefon);
            });

        }
    }
}
