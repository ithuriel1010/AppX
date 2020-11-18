using Android.Content.Res;
using AppX.DatabaseClasses;
using AppX.Utils;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AppX
{
    public class AddPatientInfoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        PatientDB patient = new PatientDB();
        public string photo = "smile";
        private AddPerson p = new AddPerson();
        private AddPatientInfo ap = new AddPatientInfo();

        string firstName;
        string lastName;
        string phoneNumber;
        DateTime birthDate;
        string hobby;
        App app;

        private string errorMessage { get; set; }
        private bool correctName { get; set; }
        private bool correctLastName { get; set; }
        private bool correctPhone { get; set; }
        private bool correctHobby { get; set; }

        private Color nameTextColor = Color.Red;
        private Color lastNameTextColor = Color.Red;
        private Color phoneTextColor = Color.Red;
        private Color hobbyTextColor = Color.Red;

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
        public Color HobbyTextColor
        {
            get => hobbyTextColor;
            set
            {
                hobbyTextColor = value;
                var args = new PropertyChangedEventArgs(nameof(HobbyTextColor));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public string MaxDate = DateTime.Now.ToString("MM/dd/yyyy");
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command PhotoCommand { get; }

        public AddPatientInfoViewModel(App app)
        {
            this.app = app;
            SaveCommand = new Command(async () =>
            {
                if (correctName && correctLastName && correctPhone && correctHobby)
                {
                    patient.HaveData = true;
                    patient.FirstName = FirstName;
                    patient.LastName = LastName;
                    patient.PhoneNumber = PhoneNumber;
                    patient.BirthDate = BirthDate;
                    patient.Hobby = Hobby;
                    patient.Photo = photo;
                    patient.LocalizationMinutes = 30;
                    patient.FallSeconds = 60;

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<PatientDB>();
                        conn.Insert(patient);
                    }

                    app.SetHomePage();
                }
                else 
                {
                    ErrorMessage = "Co najmniej jedno z pól jest nieprawidłowo wypełnione";
                }     
            });

            PhotoCommand = new Command(async () =>
            {
                photo = await ap.UploadPhoto();
            });

        }

        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                var args = new PropertyChangedEventArgs(nameof(FirstName));

                PropertyChanged?.Invoke(this, args);
                (NameTextColor, correctName) = RegexUtill.Check(RegexUtill.MinLength(3), value);

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
        public string Hobby
        {
            get => hobby;
            set
            {
                hobby = value;
                var args = new PropertyChangedEventArgs(nameof(Hobby));

                PropertyChanged?.Invoke(this, args);
                (HobbyTextColor, correctHobby) = RegexUtill.Check(RegexUtill.MinLength(3), value);

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
