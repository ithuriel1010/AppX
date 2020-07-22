using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Android.Telephony;
using static Android.Provider.Telephony;
using SmsMessage = Xamarin.Essentials.SmsMessage;
using Sms = Xamarin.Essentials.Sms;
using Android;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android.Content.PM;
using System.Collections.ObjectModel;
using SQLite;
using AppX.Classes;
using AppX.DatabaseClasses;
using Java.Security;

namespace AppX
{
    public class DetailPageViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<ContactsDB> contacts = new List<ContactsDB>();

        public DetailPageViewModel(string note)
        {
            DismissPageCommand = new Command(async () =>
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }); //Nieużywane

            SendTextCommand = new Command(async () =>
            {
                using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
                {
                    conn.CreateTable<ContactsDB>();
                    contacts = conn.Table<ContactsDB>().ToList();
                }

                foreach (var contact in contacts)
                {
                    if (!string.IsNullOrEmpty(NoteText))
                    {
                        //Send(NoteText, kontakt.Telefon);
                        SendTextAndEmail s = new SendTextAndEmail(NoteText, contact.Telefon, contact.Email);
                    }
                }               
                
            });

            NoteText = note;
        }

        public void Send(String message, String number)
        {
            try
            {                
                SmsManager.Default.SendTextMessage(number, null, message, null, null);
            }
            catch (FeatureNotSupportedException ex)
            {
                Application.Current.MainPage.Navigation.PopAsync();

                Page p = new Page();

                 p.DisplayAlert("Failed", "Sms is not supported on this device.", "OK");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.Navigation.PopAsync();

                Page p = new Page();

                p.DisplayAlert("failed", ex.Message, "ok");
            }
            
        }   //Nieużywane
        public async Task Sendsms(string messageText, string recipient)
        {
            try
            {
                var message = new SmsMessage(messageText, recipient);
                await Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                Page p = new Page();

                await p.DisplayAlert("Failed", "Sms is not supported on this device.", "OK");
            }
            catch (Exception ex)
            {
                Page p = new Page();

                await p.DisplayAlert("Failed", ex.Message, "OK");
            }
        }   //Nieużywane

        string noteText;
        public string NoteText
        {
            get => noteText;
            set
            {
                noteText = value;
                var args = new PropertyChangedEventArgs(nameof(NoteText));

                PropertyChanged?.Invoke(this, args);
            }
        }

        public Command DismissPageCommand { get; }
        public Command SendTextCommand { get; }
    }
}
