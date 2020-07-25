using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppX.LocalizationFiles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Localization : ContentPage
    {

        public Localization()
        {
            InitializeComponent();
            BindingContext = this;

            if (Accelerometer.IsMonitoring)
                return;

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.UI);

            if (Gyroscope.IsMonitoring)
                return;

            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
            Gyroscope.Start(SensorSpeed.UI);
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var data = e.Reading;
            XA.Text = "X:" + data.Acceleration.X.ToString();
            YA.Text = "Y:" + data.Acceleration.Y.ToString();
            ZA.Text = "Z:" + data.Acceleration.Z.ToString();
            // Process Acceleration X, Y, and Z
        }

        void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            var data = e.Reading;
            XG.Text = "X:" + data.AngularVelocity.X.ToString();
            YG.Text = "Y:" + data.AngularVelocity.Y.ToString();
            ZG.Text = "Z:" + data.AngularVelocity.Z.ToString();
        }
    }
}