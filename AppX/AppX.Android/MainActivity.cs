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

namespace AppX.Droid
{
    [Activity(LaunchMode = LaunchMode.SingleTop, Label = "AppX", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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
                /// Do something now that you know the user clicked on the notification...

                var notificationMessage = intent.Extras.GetString("message");
                var winnerToast = Toast.MakeText(this, $"{notificationMessage}.\n\n🍣 Please send 2 BitCoins to SushiHangover to process your winning ticket! 🍣", ToastLength.Long);
                winnerToast.SetGravity(Android.Views.GravityFlags.Center, 0, 0);
                winnerToast.Show();
            }
            else if (intent.Action == "FallAlert") //&& intent.HasExtra("MessageFromSushiHangover"))
            {
                /*MainPage mp = new MainPage();
                mp.ClickedNotification();*/
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
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.SendSms) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.SendSms }, 1);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.AccessFineLocation }, 1);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.SetOrientation) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.SetOrientation }, 1);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, 1);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.WriteExternalStorage }, 1);
            }
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != Permission.Granted)
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.Camera }, 1);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


    }
}