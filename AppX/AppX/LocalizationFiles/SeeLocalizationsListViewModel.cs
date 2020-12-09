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

            SelectedLocalizationChangedCommand = new Command(async () =>        //When user clicks on one of the localization on the list the page with localization details is created and displayed
            {
                if (SelectedLocalization == null) return;

                var localDetailVM = new LocalizationDetailsViewModel(SelectedLocalization);

                var localDetailPage = new LocalizationDetails();

                localDetailPage.BindingContext = localDetailVM;

                await Application.Current.MainPage.Navigation.PushAsync(localDetailPage);
                SelectedLocalization = null;        //Selected localization has to be made null, otherwise user cannot choose the same localization twice in a row

            });
        }


        public LocalizationsDB SelectedLocalization     //Selected localization is a localization from the list that is clicked on
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
