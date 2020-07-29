﻿using AppX.DatabaseClasses;
using System;
using System.Collections.Generic;
using System.Text;

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

        public LocalizationDetailsViewModel(LocalizationsDB localization)
        {
            this.localization = localization;
            address = localization.Address;
            lat = localization.Lat;
            lon = localization.Lon;
            name = localization.Name;
            message = localization.Message;
        }
    }
}
