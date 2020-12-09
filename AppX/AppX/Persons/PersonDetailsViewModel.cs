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

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public DateTime birthDate { get; set; }
        public string relationship { get; set; }
        public string photo { get; set; }
        public int age { get; set; }

        public Command EditCommand { get; }
        public Command QuickCall { get; }
        private PersonDetails p = new PersonDetails();


        public PersonDetailsViewModel(PersonsDB person)     //Contact in the argument is a contact sent fom method that creates a new page after clicking on a contact on the list of contacts
        {           
            this.person = person;
            firstName = person.FirstName;
            lastName = person.LastName;
            fullName = firstName + " " + lastName;
            phoneNumber = person.PhoneNumber;
            birthDate = person.BirthDate;
            relationship = person.Relationship;
            photo = person.Photo;
            age = person.Age;

            EditCommand = new Command(async () =>
            {
                p.Edit(person);

            });

            QuickCall = new Command(() =>
            {
                p.PlacePhoneCall(phoneNumber);
            });

        }
    }
}
