using AppX.DatabaseClasses;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppX
{
    public class SeeContactsListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ContactsDB> contactsList { get; set; }
        public Command SelectedContactChangedCommand { get; }

        ContactsDB selectedContact;
        public SeeContactsListViewModel()
        {
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<ContactsDB>();
                var contacts = conn.Table<ContactsDB>().ToList();

                contactsList = new ObservableCollection<ContactsDB>(contacts);
            }

            SelectedContactChangedCommand = new Command(async () =>     //When user clicks on one of the contacts on the list the page with contact details is created and displayed
            {
                if (SelectedContact == null) return;

                var contDetailVM = new ContactDetailsViewModel(SelectedContact);

                var contDetailPage = new ContactDetails();

                contDetailPage.BindingContext = contDetailVM;

                await Application.Current.MainPage.Navigation.PushAsync(contDetailPage);
                SelectedContact = null;         //Selected contact has to be made null, otherwise user cannot choose the same contact twice in a row

            });
        }

        public ContactsDB SelectedContact       //Selected contact is a contact from the list that is clicked on
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                var args = new PropertyChangedEventArgs(nameof(SelectedContact));

                PropertyChanged?.Invoke(this, args);
            }
        }
    }
}
