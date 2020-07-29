using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppX
{
    public partial class App : Application
    {
        public static string FilePath;
        public bool patientInfo;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string filePath)
        {
            InitializeComponent();
            var addPatVM = new AddPatientInfoViewModel();
            var addPatPage = new AddPatientInfo(this);

            addPatPage.BindingContext = addPatVM;

            if (!patientInfo)
                MainPage = addPatPage;
            else
                SetHomePage();

           
            //MainPage = new NavigationPage(new MainPage());
            FilePath = filePath;
        }

        public void SetHomePage()
        {
            MainPage = new NavigationPage(new MainPage());
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
