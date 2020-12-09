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

namespace AppX.Persons
{
    public class EditPersonViewModel : INotifyPropertyChanged
    {
        //Works almost exactly like the adding page, but the data of the person is already in the entry fields
        public event PropertyChangedEventHandler PropertyChanged;
        PersonsDB person;
        public string photo = "smile";
        private AddPerson p = new AddPerson();
        private EditPerson e = new EditPerson();

        string imie;
        string nazwisko;
        string telefon;
        DateTime dataUrodzenia;
        string zwiazek;

        public string MaxDate = DateTime.Now.ToString("MM/dd/yyyy");


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command PhotoCommand { get; }
        public Command ShowPhoto { get; }
        public Command DeleteCommand { get; }
        private string errorMessage { get; set; }
        private bool correctName { get; set; }
        private bool correctLastName { get; set; }
        private bool correctPhone { get; set; }
        private bool correctRelationship { get; set; }

        private Color nameTextColor = Color.Red;
        private Color lastNameTextColor = Color.Red;
        private Color phoneTextColor = Color.Red;
        private Color relationshipTextColor = Color.Red;

        public Color NameTextColor
        {
            get => nameTextColor;
            set
            {
                nameTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(NameTextColor));     // After each change in entry field the text color is checked and updated

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

        public string Imie
        {
            get => imie;
            set
            {
                imie = value;
                var args = new PropertyChangedEventArgs(nameof(Imie));

                PropertyChanged?.Invoke(this, args);
                (NameTextColor, correctName) = RegexUtill.Check(RegexUtill.MinLength(3), value);         //After each change in data field the validation is checked so the text color can be changed (here validation is at least 3 characters)

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
        public EditPersonViewModel(PersonsDB personDetails)
        {
            person = personDetails;
            Imie= person.FirstName;
            Nazwisko = person.LastName;
            Telefon = person.PhoneNumber;
            DataUrodzenia = person.BirthDate;
            Zwiazek = person.Relationship;
            photo = person.Photo;

            SaveCommand = new Command(async () =>
            {
                if (correctName && correctLastName && correctPhone && correctRelationship)
                {
                    person.FirstName = Imie;
                    person.LastName = Nazwisko;
                    person.PhoneNumber = Telefon;
                    person.BirthDate = DataUrodzenia;
                    person.Relationship = Zwiazek;
                    person.Photo = photo;

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<PersonsDB>();
                        conn.Update(person);
                    }

                    await Application.Current.MainPage.Navigation.PopToRootAsync();
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

            DeleteCommand = new Command(async () =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<PersonsDB>();
                    conn.Delete(person);
                }
                await Application.Current.MainPage.Navigation.PopToRootAsync();

            });
        }

    }

}
