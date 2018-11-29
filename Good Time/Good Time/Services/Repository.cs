using Good_Time.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Good_Time.Util;
using System.Threading.Tasks;


namespace Good_Time.Services
{
    public class Repository
    {
        public string token { get; set; }

        HttpClient Dbclient;
        HttpClient Fcmclient;
        private static Repository instance = null;
        private static readonly object padlock = new object();

        private const string webApiKey = "AIzaSyChv5UH4gQ3z95s3k9hmkoP67nHDepoKR8";
        private const string addUserURL = "https://firestore.googleapis.com/v1beta1/projects/good-time-465f6/databases/(default)/documents/users?&key=AIzaSyChv5UH4gQ3z95s3k9hmkoP67nHDepoKR8";
        private const string addPingURL = "https://firestore.googleapis.com/v1beta1/projects/good-time-465f6/databases/(default)/documents/pings?&key=AIzaSyChv5UH4gQ3z95s3k9hmkoP67nHDepoKR8";
        private const string getUserURL = "https://firestore.googleapis.com/v1beta1/projects/good-time-465f6/databases/(default)/documents/users?&key=AIzaSyChv5UH4gQ3z95s3k9hmkoP67nHDepoKR8";
        private const string sendFcmUrl = "https://fcm.googleapis.com/fcm/send";
        private const string getPingURL = "https://firestore.googleapis.com/v1beta1/projects/good-time-465f6/databases/(default)/documents:runQuery";



        public static Repository Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Repository();
                    }
                    return instance;
                }
            }
        }

        Repository()
        {
            Dbclient = new HttpClient();
            Dbclient.MaxResponseContentBufferSize = 256000;

            var serverKey = "AAAAy9pinPs:APA91bHTaQKZ3rmjlqyHzn38zAkH8aHlvb6dLRK-RXjwftlrmuNbeBmtnniVwO1YAbfuHe59Qy5g7YLw1yOiC0OK43HnxmQsX-fFsUdPFUIi-XsM5fSSIuLESew2kPL20XiVTmMtqtG9";
            Fcmclient = new HttpClient();
            Fcmclient.MaxResponseContentBufferSize = 256000;
            Fcmclient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "key =" + serverKey);
        }

        public async Task<bool> uploadUserAsync(User user)
        {
            DbUserfields f = new DbUserfields(user);
            var uri = new Uri(string.Format(addUserURL, string.Empty));

            var json = JsonConvert.SerializeObject(f);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await Dbclient.PostAsync(uri, content);

            Console.WriteLine("omer: " + response);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> sendPing(Ping p)
        {

            DbPingfields f = new DbPingfields(p);
            var uri = new Uri(string.Format(addPingURL, string.Empty));

            var json = JsonConvert.SerializeObject(f);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await Dbclient.PostAsync(uri, content);

            Console.WriteLine("omer: " + response);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> sendPushNotification(string fromName, string token)
        {
            FcmRootobject f = new FcmRootobject(fromName, token);
            var uri = new Uri(string.Format(sendFcmUrl, string.Empty));

            var json = JsonConvert.SerializeObject(f);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await Fcmclient.PostAsync(uri, content);

            Console.WriteLine("omer: " + response);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public async Task<UserResponseRootobject> getUsers(IUsersResponseListener usersResponseListener)
        //{

        //    var uri = new Uri(string.Format(getUserURL, string.Empty));

        //    var response = await client.GetAsync(uri);
        //    Console.WriteLine("omer: " + response);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var content = await response.Content.ReadAsStringAsync();

        //        UserResponseRootobject r = JsonConvert.DeserializeObject<UserResponseRootobject>(content);

        //            usersResponseListener.onUserResponse(r);

        //        return r;
        //    }
        //    return null;
        //}

        public async Task<List<User>> getUsers()
        {
            var uri = new Uri(string.Format(getUserURL, string.Empty));

            var response = await Dbclient.GetAsync(uri);
            Console.WriteLine("omer: " + response);
            List<User> users = new List<User>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                UserResponseRootobject r = JsonConvert.DeserializeObject<UserResponseRootobject>(content);
                if (r != null && r.documents != null)
                {
                    foreach (var i in r.documents)
                    {
                        users.Add(new User(i.fields.name.stringValue, i.fields.number.stringValue, "", i.fields.token.stringValue));
                    }
                }
            }

            return users;
        }


        public async Task<List<Ping>> getPings()
        {
            StructuredqueryRootobject f = new StructuredqueryRootobject();
            f.structuredQuery = new Structuredquery();
            f.structuredQuery.from = new From[] { new From() };
            f.structuredQuery.from[0].collectionId = "pings";
            f.structuredQuery.where = new Where();
            f.structuredQuery.where.fieldFilter = new Fieldfilter();
            f.structuredQuery.where.fieldFilter.field = new Field();
            f.structuredQuery.where.fieldFilter.field.fieldPath = "to";
            f.structuredQuery.where.fieldFilter.value = new Value();
            f.structuredQuery.where.fieldFilter.value.stringValue = FileSystemHelper.Instance.loadCardentials().number;
            f.structuredQuery.where.fieldFilter.op = "EQUAL";

            var uri = new Uri(string.Format(getPingURL, string.Empty));
            var json = JsonConvert.SerializeObject(f);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await Dbclient.PostAsync(uri, content);
            Console.WriteLine("omer: " + response);
            List<Ping> pings = new List<Ping>();

            if (response.IsSuccessStatusCode)
            {
                var c = await response.Content.ReadAsStringAsync();
                PingRootobject p = JsonConvert.DeserializeObject<PingRootobject>(c);
                if (p != null && p.Property1 != null)
                {
                    foreach (var i in p.Property1)
                    {
                        pings.Add(new Ping(i.document.fields.from.stringValue, i.document.fields.to.stringValue, i.document.fields.endTime.stringValue));
                    }

                }
                return pings;
            }
            return null;
        }

        public static Dictionary<String, Object> parse(byte[] json)
        {
            string jsonStr = Encoding.UTF8.GetString(json);
            return JsonConvert.DeserializeObject<Dictionary<String, Object>>(jsonStr);
        }

        private class DbUserfields
        {
            public DbUser fields;

            public DbUserfields(User user)
            {
                fields = new DbUser(user);
            }
        }

        private class DbPingfields
        {
            public DbPing fields;

            public DbPingfields(Ping p)
            {
                fields = new DbPing(p);
            }
        }

        private class DbStringValue
        {
            public string stringValue;

            public DbStringValue(string val)
            {
                stringValue = val;
            }
        }

        private class DbUser
        {
            public DbStringValue name;
            public DbStringValue number;
            public DbStringValue token;

            public DbUser(User user)
            {
                this.name = new DbStringValue(user.name);
                this.number = new DbStringValue(user.number);
                this.token = new DbStringValue(user.token);
            }
        }

        private class DbPing
        {
            public DbStringValue from;
            public DbStringValue to;
            public DbStringValue endTime;

            public DbPing(Ping ping)
            {
                this.from = new DbStringValue(ping.from);
                this.to = new DbStringValue(ping.to);
                this.endTime = new DbStringValue(ping.endTime);
            }
        }





    }
}

