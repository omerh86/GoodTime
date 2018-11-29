using System;

namespace Good_Time.Models
{
    public class Ping
    {
        public string from { get; set; }
        public string to { get; set; }
        public string endTime { get; set; }


        public Ping(string from, string to, string endTime)
        {
            this.from = from;
            this.to = to;
            this.endTime = endTime;

        }

    }
}