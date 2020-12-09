using AppX.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AppX.LocalizationFiles
{
    public class LocalizationDetailsViewModel
    {
        public LocalizationsDB localization;
        public string address { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string name { get; set; }
        public string message { get; set; }

        public Command EditCommand { get; }
        private LocalizationDetails localizationDetailsPage = new LocalizationDetails();

        public LocalizationDetailsViewModel(LocalizationsDB localization)       //Localization in the argument is a localization sent fom method that creates a new page after clicking on a localization on the list of all localizations
        {
            this.localization = localization;

            EditCommand = new Command(async () =>
            {
                localizationDetailsPage.Edit(localization);

            });

            address = localization.Address;
            lat = localization.Lat;
            lon = localization.Lon;
            name = localization.Name;
            message = localization.Message;
        }
    }
}
