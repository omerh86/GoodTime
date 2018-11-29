using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Good_Time.Models;
using Good_Time.Util;

namespace Good_Time.Services
{
    public class ContactService : IUsersResponseListener
    {
        public IContactListener contactListener { set; get; }

        private List<User> phoneBookUsers;
        private List<User> _serverUsers;
        public List<User> serverUsers
        {
            get
            {
                return _serverUsers;
            }
            set
            {
                _serverUsers = value;
                fireOnUserChanged(value, true);
            }
        }

        private static ContactService instance = null;
        private static readonly object padlock = new object();

        public static ContactService Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ContactService();
                    }
                    return instance;
                }
            }
        }

        ContactService()
        {
            this.phoneBookUsers = new List<User>();
            this.serverUsers = new List<User>();

            var contacts = FileSystemHelper.Instance.loadContacts();

            if (contacts != null && contacts.Count > 0)
            {
                this.phoneBookUsers = contacts;
            }

            loadAsyncContacts();
            loadAsyncUsers();
        }


        public List<User> getUsers(bool isServerUser)
        {
            if (isServerUser)
            {
                return serverUsers;
            }
            else
            {
                return phoneBookUsers;
            }

        }

        private async Task loadAsyncUsers()
        {
            serverUsers = await Repository.Instance.getUsers();
            //  fireOnUserChanged(serverUsers, true);
        }

        private async Task loadAsyncContacts()
        {
            var contacts = await Plugin.ContactService.CrossContactService.Current.GetContactListAsync();
            foreach (var c in contacts)
            {
                if (c.Number != null && c.Number.Length > 7)
                {
                    this.phoneBookUsers.Add(new User(c.Name, c.Number, c.PhotoUriThumbnail, null));
                }
            }
            fireOnUserChanged(this.phoneBookUsers, false);
            FileSystemHelper.Instance.saveContacts(this.phoneBookUsers);
        }

        private void fireOnUserChanged(List<User> items, bool isServerUser)
        {
            if (contactListener != null)
                contactListener.onUserChanged(items, isServerUser);
        }

        public void onUserResponse(UserResponseRootobject r)
        {
            var users = new List<User>();
            if (r != null && r.documents != null)
            {
                foreach (var i in r.documents)
                {
                    users.Add(new User(i.fields.name.stringValue, i.fields.number.stringValue, "", i.fields.token.stringValue));
                }
                this.serverUsers = users;
            }
        }
    }
}