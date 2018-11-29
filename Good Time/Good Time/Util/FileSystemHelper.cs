using Good_Time.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

using System.Text;

namespace Good_Time.Util
{
    class FileSystemHelper
    {
        private static FileSystemHelper instance = null;
        private static readonly object padlock = new object();

        public static FileSystemHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FileSystemHelper();
                    }
                    return instance;
                }
            }
        }

        public void saveCardentials(User user)
        {
            string s = "";
            if (user != null)
            {
                s = objToJson<User>(user);
            }

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "my_cardentials");
            File.WriteAllText(fileName, s);
        }

        public User loadCardentials()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "my_cardentials");
            if (!File.Exists(fileName))
            {
                return null;
            }
            string s = File.ReadAllText(fileName);
            if (s != null && s != "")
            {
                User result = jsonToObj<User>(s);
                return result;
            }
            return null;
        }

        public void saveContacts(List<User> contacts)
        {
            string s = "";
            if (contacts != null)
            {
                s = objToJson<List<User>>(contacts);
            }

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "my_contacts");
            File.WriteAllText(fileName, s);
        }

        public List<User> loadContacts()
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "my_contacts");
            if (!File.Exists(fileName))
            {
                return null;
            }
            string s = File.ReadAllText(fileName);
            if (s != null && s != "")
            {
                List<User> result = jsonToObj<List<User>>(s);
                return result;
            }
            return null;
        }

        public T jsonToObj<T>(string val)
        {
            T loginInfo = JsonConvert.DeserializeObject<T>(val);
            return loginInfo;
        }

        public string objToJson<T>(T obj)
        {
            string res = JsonConvert.SerializeObject(obj);
            return res;
        }
    }
}
