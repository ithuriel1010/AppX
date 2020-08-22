using AppX.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppX.LocalizationFiles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocalizationDetails : ContentPage
    {
        public static LocalizationsDB localization;
        public LocalizationDetails()
        {
            InitializeComponent();
        }

        public async void Edit(LocalizationsDB localizationDetails)
        {
            localization = localizationDetails;
            var editLocalizationVM = new EditLocalizationViewModel(localization);
            var editLocalizationPage = new EditLocalization();

            editLocalizationPage.BindingContext = editLocalizationVM;
            await Application.Current.MainPage.Navigation.PushAsync(editLocalizationPage);
        }
    }
}