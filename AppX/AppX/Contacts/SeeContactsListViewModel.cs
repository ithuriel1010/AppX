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

            SelectedContactChangedCommand = new Command(async () =>
            {
                if (SelectedContact == null) return;

                var contDetailVM = new ContactDetailsViewModel(SelectedContact);

                var contDetailPage = new ContactDetails();

                contDetailPage.BindingContext = contDetailVM;

                await Application.Current.MainPage.Navigation.PushAsync(contDetailPage);
                SelectedContact = null;

            });
        }

        public ContactsDB SelectedContact
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
