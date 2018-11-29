using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android;
using Firebase;
using Firebase.Database;
using Android.Telephony;
using Good_Time.Models;
using Xamarin.Forms;

namespace Good_Time.Droid
{
    [Activity(Label = "Good_Time", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        int PERMISSIONS_REQUEST = 101;
        private const string FirebaseURL = "https://good-time-465f6.firebaseio.com/"; //Firebase Database URL  
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            getPermissions();
            FirebaseApp.InitializeApp(this);
           // getInfo();
        }

        private void getInfo()
        {
            TelephonyManager mTelephonyMgr;

            mTelephonyMgr = (TelephonyManager)GetSystemService(TelephonyService);

            var Number = mTelephonyMgr.Line1Number;


        }

        private void getPermissions()
        {
            if (Int32.Parse(global::Android.OS.Build.VERSION.Sdk) >= 23)
            {
                List<string> Permissions = new List<string>();


                if (this.CheckSelfPermission(Manifest.Permission.ReadContacts) != Permission.Granted)
                {
                    Permissions.Add(Manifest.Permission.ReadContacts);
                }

                if (this.CheckSelfPermission(Manifest.Permission.ReadPhoneState) != Permission.Granted)
                {
                    Permissions.Add(Manifest.Permission.ReadPhoneState);
                }

                if (this.CheckSelfPermission(Manifest.Permission.SendSms) != Permission.Granted)
                {
                    Permissions.Add(Manifest.Permission.SendSms);
                }

                if (this.CheckSelfPermission(Manifest.Permission.WakeLock) != Permission.Granted)
                {
                    Permissions.Add(Manifest.Permission.WakeLock);
                }
                if (Permissions.Count > 0)
                {
                    this.RequestPermissions(Permissions.ToArray(), PERMISSIONS_REQUEST);
                }

            }
        }

    }
}