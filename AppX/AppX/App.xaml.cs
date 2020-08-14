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
            var addPatVM = new AddPatientInfoViewModel(this);
            var addPatPage = new AddPatientInfo(this);

            addPatPage.BindingContext = addPatVM;

            if (!patientInfo)
                MainPage = addPatPage;
            else
                SetHomePage();

           
            //MainPage = new NavigationPage(new MainPage());
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
