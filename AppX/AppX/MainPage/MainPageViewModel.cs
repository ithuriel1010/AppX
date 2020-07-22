using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AppX
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> AllNotes { get; set; } = new ObservableCollection<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel() 
        {
            EraseCommand = new Command(() =>
            {
                TheNote = string.Empty;
            });

            SaveCommand = new Command(() =>
              {
                  AllNotes.Add(TheNote);
                  TheNote = string.Empty;
              });

            SelectedNotesChangedCommand = new Command(async () =>
            {
                if (SelectedNote == null) return;

                var detailVM = new DetailPageViewModel(SelectedNote);

                var detailPage = new DetailPage();

                detailPage.BindingContext = detailVM;

                await Application.Current.MainPage.Navigation.PushAsync(detailPage);
                
            });

            AddContact = new Command(async () =>
            {
                var addContVM = new AddContactViewModel();
                var addContPage = new AddContact();

                addContPage.BindingContext = addContVM;
                await Application.Current.MainPage.Navigation.PushAsync(addContPage);
            }); // Nieużywane

            SeeContacts = new Command(async () =>
            {
                var seeContVM = new SeeContactsListViewModel();
                var seeContPage = new SeeContactsList();

                seeContPage.BindingContext = seeContVM;
                await Application.Current.MainPage.Navigation.PushAsync(seeContPage);

            }); // Nieużywane

        }


        string theNote;
        public string TheNote
        {
            get => theNote;
            set
            {
                theNote = value;
                var args = new PropertyChangedEventArgs(nameof(TheNote));

                PropertyChanged?.Invoke(this, args);
            }
        }

        string selectedNote;

        public string SelectedNote
        {
            get => selectedNote;
            set
            {
                selectedNote = value;
                var args = new PropertyChangedEventArgs(nameof(SelectedNote));

                PropertyChanged?.Invoke(this, args);
            }
        }



        public Command SaveCommand { get; }
        public Command EraseCommand { get; }
        public Command SelectedNotesChangedCommand { get; }
        public Command AddContact { get; }
        public Command SeeContacts { get; }
        public Command Add { get; }

    }
}
