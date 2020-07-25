using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace AppX.LocalizationFiles
{
    public class LocalizationViewModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //SensorSpeed speed = SensorSpeed.UI;

        public string Lokalizacja { get; set; }
        public string Acc { get; set; }


        AccelerometerTest at = new AccelerometerTest();
        public LocalizationViewModel(string Lokalizacja)
        {
            //Accelerometer.Start(speed);
            //this.Acc = at.Acc;
            this.Lokalizacja = Lokalizacja;
        }
    }

    public class AccelerometerTest
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;
        public string Acc { get; set; }

        public AccelerometerTest()
        {
            // Register for reading changes, be sure to unsubscribe when finished
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            Acc = $"Reading: X: {data.Acceleration.X}, Y: {data.Acceleration.Y}, Z: {data.Acceleration.Z}";
            // Process Acceleration X, Y, and Z
        }

        public void ToggleAccelerometer(string acc)
        {
            acc = Acc;
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}
