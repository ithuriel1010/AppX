﻿using AppX.DatabaseClasses;
using SQLite;
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

            
            Device.SetFlags(new string[] { "Brush_Experimental" });
            MainPage = new NavigationPage(new MainPage());
        }

        public App(string filePath)
        {
            FilePath = filePath;
            PatientDB patient;

            using (SQLiteConnection conn = new SQLiteConnection(FilePath))
            {
                conn.CreateTable<PatientDB>();
                patient = conn.Table<PatientDB>().FirstOrDefault();
            }

            if(patient == null)
            {
                patientInfo = false;
            }
            else
            {
                patientInfo = patient.HaveData;
            }

            InitializeComponent();
            Device.SetFlags(new string[] { "Brush_Experimental" });

            var addPatVM = new AddPatientInfoViewModel(this);
            var addPatPage = new AddPatientInfo(this);

            addPatPage.BindingContext = addPatVM;

            var Color1 = Application.Current.Resources["Primary"];
            Color PrimaryColor = (Color)Color1;

            if (!patientInfo)
                MainPage = new NavigationPage(addPatPage)
                {
                    BarBackgroundColor = PrimaryColor,
                    BarTextColor = Color.Black
                };
            else
                SetHomePage();

           
            //MainPage = new NavigationPage(new MainPage());
        }

        public void SetHomePage()
        {
            var Color1 = Application.Current.Resources["Primary"];
            Color PrimaryColor = (Color)Color1;
            var mainPage = new MainPage();
            MainPage = new NavigationPage(mainPage)
            {
                BarBackgroundColor = PrimaryColor,
                BarTextColor = Color.Black
            };
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
