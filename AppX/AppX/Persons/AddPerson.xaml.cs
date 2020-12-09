using Android.Graphics;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class AddPerson : ContentPage
    {
        public string photo;

        public AddPerson()
        {
            InitializeComponent();
            BindingContext = this;
            data.MaximumDate = DateTime.Now;
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
                }
                else
                {
                    photo = "smile";
                }
            }
            catch
            {
                App.Current.MainPage.DisplayAlert("Brak zezwoleń!", "Zezwól aplikacji na dostęp do mediów aby przesłać zdjęcie", "Ok");
            }

            return photo;
        }

    }
}