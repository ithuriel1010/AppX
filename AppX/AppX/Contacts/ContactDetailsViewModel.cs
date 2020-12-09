using AppX.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppX
{
    public class ContactDetailsViewModel
    {
        private ContactDetails contactDetailsPage = new ContactDetails();

        public ContactsDB contact;

        string firstName;
        string lastName;
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string relationship { get; set; }
        public Command QuickCall  { get; }
        public Command EditCommand { get; }
        public ContactDetailsViewModel(ContactsDB contact)      //Contact in the argument is a contact sent fom method that creates a new page after clicking on a contact on the list of contacts
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
                contactDetailsPage.Edit(contact);

            });

            QuickCall = new Command(() =>
            {
                contactDetailsPage.PlacePhoneCall(phoneNumber);
            });

        }
    }
}
