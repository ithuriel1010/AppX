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
        string firstName;
        string lastName;
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string relationship { get; set; }
        public Command QuickCall  { get; }
        public Command EditCommand { get; }
        private ContactDetails c = new ContactDetails();

        public ContactDetailsViewModel(ContactsDB contact)
        {
            this.contact = contact;
            firstName = contact.FirstName;
            lastName = contact.LastName;
            fullName = firstName + " " + lastName;
            phoneNumber = contact.PhoneNumber;
            email = contact.Email;
            relationship = contact.Relationship;

            EditCommand = new Command(async () =>
            {
                c.Edit(contact);

            });

            QuickCall = new Command(() =>
            {
                c.PlacePhoneCall(phoneNumber);
            });

        }
    }
}
