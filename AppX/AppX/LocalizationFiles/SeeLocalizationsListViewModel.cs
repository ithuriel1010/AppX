using AppX.DatabaseClasses;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AppX.LocalizationFiles
{
    class SeeLocalizationsListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<LocalizationsDB> localizationsList { get; set; }

        public SeeLocalizationsListViewModel()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<LocalizationsDB>();
                var localizations = conn.Table<LocalizationsDB>().ToList();

                localizationsList = new ObservableCollection<LocalizationsDB>(localizations);
            }

            SelectedLoalizationChangedCommand = new Command(async () =>
            {
                /*if (SelectedLocalization == null) return;

                var contDetailVM = new ContactDetailsViewModel(SelectedLocalization);

                var contDetailPage = new ContactDetails();

                contDetailPage.BindingContext = contDetailVM;

                await Application.Current.MainPage.Navigation.PushAsync(contDetailPage);
                SelectedLocalization = null;*/

            });
        }

        public Command SelectedLoalizationChangedCommand { get; }

        LocalizationsDB selectedLocalization;

        public LocalizationsDB SelectedLocalization
        {
            get => selectedLocalization;
            set
            {
                SelectedLocalization = value;
                var args = new PropertyChangedEventArgs(nameof(selectedLocalization));

                PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
