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
using AppX.Utils;
using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Contexts;
using ReactiveUI.Validation.Extensions;
using Android.Media;

namespace AppX
{
    public class AddPersonViewModel : ReactiveObject, INotifyPropertyChanged, IValidatableViewModel
    {
        public ValidationContext ValidationContext { get; } = new ValidationContext();

        public event PropertyChangedEventHandler PropertyChanged;
        PersonsDB person = new PersonsDB();
        public string photo = "smile";
        private AddPerson p = new AddPerson();

        string firstName;
        string lastName;
        string phoneNumber;
        DateTime birthDate;
        string relationship;

        public string MaxDate = DateTime.Now.ToString("MM/dd/yyyy");        //The max birth date user can set is today
        private string errorMessage { get; set; }
        private bool correctName { get; set; }
        private bool correctLastName { get; set; }
        private bool correctPhone { get; set; }
        private bool correctRelationship { get; set; }

        private Color nameTextColor = Color.Red;
        private Color lastNameTextColor = Color.Red;
        private Color phoneTextColor = Color.Red;
        private Color relationshipTextColor = Color.Red;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command PhotoCommand { get; }

        public Color NameTextColor
        {
            get => nameTextColor;
            set
            {
                nameTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(NameTextColor));     //After each change in entry field the text color is checked and updated

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

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                var args = new PropertyChangedEventArgs(nameof(FirstName));

                PropertyChanged?.Invoke(this, args);

                (NameTextColor, correctName) = RegexUtill.Check(RegexUtill.MinLength(3), value);         //After each change in data field the validation is checked so the text color can be changed (here validation is at least 3 characters)
            }
        }
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                var args = new PropertyChangedEventArgs(nameof(LastName));

                PropertyChanged?.Invoke(this, args);

                (LastNameTextColor, correctLastName) = RegexUtill.Check(RegexUtill.MinLength(3), value);

            }
        }
        public string PhoneNumber
        {
            get => phoneNumber;
            set
            {
                phoneNumber = value;
                var args = new PropertyChangedEventArgs(nameof(PhoneNumber));

                PropertyChanged?.Invoke(this, args);

                (PhoneTextColor, correctPhone) = RegexUtill.Check(RegexUtill.PhoneNumber(), value);

            }
        }
        public DateTime BirthDate
        {
            get => birthDate;
            set
            {
                birthDate = value;
                var args = new PropertyChangedEventArgs(nameof(BirthDate));

                PropertyChanged?.Invoke(this, args);
            }
        }
        public string Relationship
        {
            get => relationship;
            set
            {
                relationship = value;
                var args = new PropertyChangedEventArgs(nameof(Relationship));

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

        public AddPersonViewModel()
        {
            SaveCommand = new Command(async () =>
            {
                if(correctName && correctLastName && correctPhone && correctRelationship)       //If all data is correctly filled
                {
                    ErrorMessage = "";
                    person.FirstName = FirstName;
                    person.LastName = LastName;
                    person.PhoneNumber = PhoneNumber;
                    person.BirthDate = BirthDate;
                    person.Relationship = Relationship;
                    person.Photo = photo;

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<PersonsDB>();
                        conn.Insert(person);
                    }

                    await Application.Current.MainPage.Navigation.PopAsync();       //Go back to the main page
                }
                else
                {
                    ErrorMessage = "Co najmniej jedno z pól jest nieprawidłowo wypełnione";
                }   

            });

            PhotoCommand = new Command(async () =>
            {
                photo = await p.UploadPhoto();
                if(photo==null)
                {
                    photo = "smile";
                }
            });

            CancelCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();

            });
        }           
    }
}

