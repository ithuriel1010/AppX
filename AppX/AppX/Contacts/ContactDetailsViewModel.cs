using AppX.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppX
{
    public class ContactDetailsViewModel
    {
        public ContactsDB contact;
        string imie;
        string nazwisko;
        public string imieNazwisko { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        public string zwiazek { get; set; }
        public Command QuickCall  { get; }
        public Command EditCommand { get; }
        private ContactDetails c = new ContactDetails();

        public ContactDetailsViewModel(ContactsDB contact)
        {
            EditCommand = new Command(async () =>
            {
                c.Edit(contact);

            });

            this.contact = contact;
            imie = contact.Imie;
            nazwisko = contact.Nazwisko;
            imieNazwisko = imie + " " + nazwisko;
            telefon = contact.Telefon;
            email = contact.Email;
            zwiazek = contact.Zwiazek;
        }
    }
}
