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

        string imie;
        string nazwisko;
        string telefon;
        DateTime dataUrodzenia;
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
                    patient.Imie = Imie;
                    patient.Nazwisko = Nazwisko;
                    patient.Telefon = Telefon;
                    patient.DataUrodzenia = DataUrodzenia;
                    patient.Hobby = Hobby;
                    patient.Zdjecie = photo;
                    patient.LocalizationMinutes = 10;
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
