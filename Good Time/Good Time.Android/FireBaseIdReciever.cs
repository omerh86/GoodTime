using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using Firebase.Messaging;
using Good_Time.Services;

namespace Good_Time.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    class FireBaseIdReciever : FirebaseInstanceIdService
    {

        const string TAG = "omer40";
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Console.WriteLine("Refreshed token: " + refreshedToken);
            Good_Time.Services.Repository.Instance.token = refreshedToken;
            //sent token to server;           
        }
    }
}
