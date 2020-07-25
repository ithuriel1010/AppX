using AppX.Classes;
using AppX.Persons;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AppX
{
    class SeePersonsListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<PersonsDB> personsList { get; set; }
        public Command SelectedPersonChangedCommand { get; }

        PersonsDB selectedPerson;
        public SeePersonsListViewModel()
        {
            using(SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<PersonsDB>();
                var persons = conn.Table<PersonsDB>().ToList();

                personsList = new ObservableCollection<PersonsDB>(persons);                
            }

            foreach (var person in personsList)
            {
                person.Wiek = DateTime.Now.Year - person.DataUrodzenia.Year;
            }

            SelectedPersonChangedCommand = new Command(async () =>
            {
                if (SelectedPerson == null) return;

                var perDetailVM = new PersonDetailsViewModel(SelectedPerson);

                var perDetailPage = new PersonDetails();

                perDetailPage.BindingContext = perDetailVM;

                await Application.Current.MainPage.Navigation.PushAsync(perDetailPage);
                SelectedPerson = null;

            });
        }

        public PersonsDB SelectedPerson
        {
            get => selectedPerson;
            set
            {
                selectedPerson = value;
                var args = new PropertyChangedEventArgs(nameof(SelectedPerson));

                PropertyChanged?.Invoke(this, args);
            }
        }

    }
}
