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
using AppX.Utils;

namespace AppX
{
    public class EditContactViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ContactsDB contact;

        string imie;
        string nazwisko;
        string telefon;
        string email;
        string zwiazek;

        private string errorMessage { get; set; }
        private bool correctName { get; set; }
        private bool correctLastName { get; set; }
        private bool correctPhone { get; set; }
        private bool correctEmail { get; set; }
        private bool correctRelationship { get; set; }

        private Color nameTextColor = Color.Red;
        private Color lastNameTextColor = Color.Red;
        private Color phoneTextColor = Color.Red;
        private Color emailTextColor = Color.Red;
        private Color relationshipTextColor = Color.Red;

        public Color NameTextColor
        {
            get => nameTextColor;
            set
            {
                nameTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(NameTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public Color LastNameTextColor
        {
            get => lastNameTextColor;
            set
            {
                lastNameTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(LastNameTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public Color PhoneTextColor
        {
            get => phoneTextColor;
            set
            {
                phoneTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(PhoneTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public Color EmailTextColor
        {
            get => emailTextColor;
            set
            {
                emailTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(EmailTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public Color RelationshipTextColor
        {
            get => relationshipTextColor;
            set
            {
                relationshipTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(RelationshipTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command DeleteCommand { get; }
        public EditContactViewModel(ContactsDB contact1)
        {
            contact = contact1;
            Imie = contact.FirstName;
            Nazwisko = contact.LastName;
            Telefon = contact.PhoneNumber;
            Email = contact.Email;
            Zwiazek = contact.Relationship;

            SaveCommand = new Command(async () =>
            {
                if (correctName && correctLastName && correctPhone && correctEmail && correctRelationship)
                {
                    contact.FirstName = Imie;
                    contact.LastName = Nazwisko;
                    contact.PhoneNumber = Telefon;
                    contact.Email = Email;
                    contact.Relationship = Zwiazek;

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<ContactsDB>();
                        conn.Update(contact);
                    }

                    await Application.Current.MainPage.Navigation.PopToRootAsync();
                }
                else
                {
                    ErrorMessage = "Co najmniej jedno z pól jest nieprawidłowo wypełnione";
                }
            });

            CancelCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();

            });

            DeleteCommand = new Command(async () =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<ContactsDB>();
                    conn.Delete(contact);
                }

                await Application.Current.MainPage.Navigation.PopToRootAsync();

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
                (NameTextColor, correctName) = RegexUtill.Check(RegexUtill.MinLength(3), value);

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
                (LastNameTextColor, correctLastName) = RegexUtill.Check(RegexUtill.MinLength(3), value);

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
                (PhoneTextColor, correctPhone) = RegexUtill.Check(RegexUtill.PhoneNumber(), value);

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
                (EmailTextColor, correctEmail) = RegexUtill.Check(RegexUtill.Email(), value);
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
                (RelationshipTextColor, correctRelationship) = RegexUtill.Check(RegexUtill.MinLength(3), value);

            }
        }
        public string ErrorMessage
        {
            get => errorMessage;
            set
            {
                errorMessage = value;
                var args = new PropertyChangedEventArgs(nameof(ErrorMessage));

                PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
