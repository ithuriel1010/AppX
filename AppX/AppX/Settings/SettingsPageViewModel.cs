using AppX.DatabaseClasses;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AppX.Settings
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        PatientDB patient = new PatientDB();

        string localizationMinutes;
        string fallSeconds;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public Command ChangeDataCommand { get; }


        public SettingsPageViewModel()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<PatientDB>();
                patient = conn.Table<PatientDB>().FirstOrDefault();
            }

            LocalizationMinutes = patient.LocalizationMinutes.ToString();
            FallSeconds = patient.FallSeconds.ToString();

            SaveCommand = new Command(async () =>
            {
                if (LocalizationMinutes != null && FallSeconds != null)
                {
                    patient.LocalizationMinutes = Convert.ToInt32(LocalizationMinutes);
                    patient.FallSeconds = Convert.ToInt32(FallSeconds);

                    using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                    {
                        conn.CreateTable<PatientDB>();
                        conn.Update(patient);
                    }
                }

                await Application.Current.MainPage.Navigation.PopToRootAsync();
            });

            CancelCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            });

            ChangeDataCommand = new Command(async () =>
            {
                var editPatientPageVM = new EditPatientPageViewModel();
                var editPatientPage = new EditPatientPage();

                editPatientPage.BindingContext = editPatientPageVM;
                await Application.Current.MainPage.Navigation.PushAsync(editPatientPage);
            });



        }
        public string LocalizationMinutes
        {
            get => localizationMinutes;
            set
            {
                localizationMinutes = value;
                var args = new PropertyChangedEventArgs(nameof(LocalizationMinutes));

                PropertyChanged?.Invoke(this, args);

            }
        }
        public string FallSeconds
        {
            get => fallSeconds;
            set
            {
                fallSeconds = value;
                var args = new PropertyChangedEventArgs(nameof(FallSeconds));

                PropertyChanged?.Invoke(this, args);

            }
        }
    }
}
