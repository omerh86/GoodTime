using System;

namespace Good_Time.Models
{
    public class User
    {
        public string name { get; set; }
        public string number { get; set; }
        public string photoUri { get; set; }
        public string token { get; set; }
        public bool isPingAvaliable { get; set; }

        public User(string name, string number, string photo, string token)
        {
            this.name = name;
            this.number = number;
            this.photoUri = photo;
            this.token = token;
            if (token != null && token.Length > 0)
                isPingAvaliable = true;

        }

    }
}