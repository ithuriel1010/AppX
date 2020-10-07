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
    public partial class AreYouOk : ContentPage
    {
        public AreYouOk()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            MainPage.noAnwser = false;
            //mp.ClickedNotification();
        }

        public async void EverythingOk(object sender, EventArgs args)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public void NotFine(object sender, EventArgs args)
        {
            SendTextAndEmail s = new SendTextAndEmail("Upadek", "+48604051870", "ithuriel1010@gmail.com");
        }
    }
}