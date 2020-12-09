using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Telephony;
using Android;
using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Content.Res;
using System.Linq;
using Android.Support.Design.Widget;

namespace AppX.Droid
{
    [Activity(LaunchMode = LaunchMode.SingleTop, Label = "Mój asystent", Icon = "@drawable/brain", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            NotificationClickedOn(intent);
        }
        void NotificationClickedOn(Intent intent)
        {
            if (intent.Action == "TestMessage") //&& intent.HasExtra("MessageFromSushiHangover"))
            {
            }
            else if (intent.Action == "FallAlert") //If it was the fall detection notification
            {
                App.SetPage();
            }
        }



        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            RequestForPermissions();

            string fileName = "database.db3";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string completePath = Path.Combine(folderPath, fileName);

            LoadApplication(new App(completePath));
        }

        private void RequestForPermissions()
        {
            var permissionsToRequest = new List<string>();
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.SendSms) != Permission.Granted)
            {
                permissionsToRequest.Add(Manifest.Permission.SendSms);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Permission.Granted)
            {
                permissionsToRequest.Add(Manifest.Permission.AccessFineLocation);
            }

            if (permissionsToRequest.Any())         //Ask for nrcessary permisions all at once
                ActivityCompat.RequestPermissions(this, permissionsToRequest.ToArray(), 1);
        }
            

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }
}