using AppX.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppX
{
    class SeePersonsListViewModel
    {
        public ObservableCollection<PersonsDB> personsList { get; set; }
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
        }

    }
}
