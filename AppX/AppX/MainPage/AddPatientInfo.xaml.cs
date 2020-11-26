using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppX
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPatientInfo : ContentPage
    {
        private App app;
        public string photo;


        public AddPatientInfo()
        {
            InitializeComponent();
        }

        public AddPatientInfo(App app)
        {
            InitializeComponent();
            this.app = app;
        }

        public void Save()
        {
            //app.patientInfo = true;

            //app.SetHomePage();

            //await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async void ClickAction(object sender, EventArgs e)
        {
            
        }
        public async Task<string> UploadPhoto()
        {
            try
            {                
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {

                }


                var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                    CompressionQuality = 40
                });


                if (file != null)
                {
                    photo = file.Path;
                    //ikona.Source = photo;
                }
                else
                {
                    photo = "smile";
                }

                //var bitmap = new Image { Source = photo };

                //DisplayIcon.Source = bitmap.Source;

            }
            catch(Exception ex) 
            {
                photo = "smile";

                App.Current.MainPage.DisplayAlert("Brak zezwoleń!", "Zezwól aplikacji na dostęp do mediów aby przesłać zdjęcie", "Ok");
            }

            return photo;
        }
    }

    public class NotificationEventArgs : EventArgs
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }
}