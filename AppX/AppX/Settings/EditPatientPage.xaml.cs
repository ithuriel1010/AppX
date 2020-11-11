using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppX.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPatientPage : ContentPage
    {
        public string photo;

        public EditPatientPage()
        {
            InitializeComponent();
        }

        public async Task<string> UploadPhoto()
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

            return photo;
        }
    }
}