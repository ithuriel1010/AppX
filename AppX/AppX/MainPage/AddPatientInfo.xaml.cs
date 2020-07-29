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

        public AddPatientInfo()
        {
            InitializeComponent();
        }

        public AddPatientInfo(App app)
        {
            InitializeComponent();
            this.app = app;
        }

        void Save(object sender, EventArgs e)
        {
            app.patientInfo = true;

            app.SetHomePage();

            //await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}