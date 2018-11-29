using System;
using System.Collections.Generic;
using System.Text;

namespace Good_Time.Models
{
    class UserGroups : List<User>
    {
        public string Title { get; set; }

        public UserGroups(string title)
        {
            Title = title;
        }
    }

}

