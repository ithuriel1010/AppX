using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppX
{
    public class AddPatientInfoViewModel
    {
        public Command SaveCommand { get; }

        public AddPatientInfoViewModel()
        {
            SaveCommand = new Command(async () =>
            {
                //App.patientInfo = true;
                //var MainPage = new NavigationPage(new MainPage());

                //await Application.Current.MainPage.Navigation.PopAsync();


            });
        }
    }
}
