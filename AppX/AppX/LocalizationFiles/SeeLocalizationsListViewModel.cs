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
    public class SeeLocalizationsListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<LocalizationsDB> localizationsList { get; set; }
        public Command SelectedLocalizationChangedCommand { get; }

        LocalizationsDB selectedLocalization;
        public SeeLocalizationsListViewModel()
        {
            using (SQLiteConnection loc = new SQLiteConnection(App.FilePath))
            {
                loc.CreateTable<LocalizationsDB>();
                var localizations = loc.Table<LocalizationsDB>().ToList();

                localizationsList = new ObservableCollection<LocalizationsDB>(localizations);
            }

            SelectedLocalizationChangedCommand = new Command(async () =>
            {
                if (SelectedLocalization == null) return;

                var localDetailVM = new LocalizationDetailsViewModel(SelectedLocalization);

                var localDetailPage = new LocalizationDetails();

                localDetailPage.BindingContext = localDetailVM;

                await Application.Current.MainPage.Navigation.PushAsync(localDetailPage);
                SelectedLocalization = null;

            });
        }


        public LocalizationsDB SelectedLocalization
        {
            get => selectedLocalization;
            set
            {
                selectedLocalization = value;
                var args = new PropertyChangedEventArgs(nameof(SelectedLocalization));

                PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
