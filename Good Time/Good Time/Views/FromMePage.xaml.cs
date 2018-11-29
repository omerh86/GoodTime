using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Good_Time.Models;
using Good_Time.Views;

using Good_Time.Services;
using Plugin.Messaging;
using Xamarin.Essentials;
using Android.Telephony;
using Good_Time.Util;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Good_Time.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FromMePage : ContentPage, IContactListener, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsBusy { get; set; }

        private List<UserGroups> groupedUsers;
        UserGroups users;
        UserGroups contacts;
        public Command LoadItemsCommand { get; set; }


        public FromMePage()
        {
            InitializeComponent();
            users = new UserGroups("Users (TB)");
            contacts = new UserGroups("Contacts (sms)");
            initGroupUser();
            groupedUsers.Add(users);
            groupedUsers.Add(contacts);

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            ContactService.Instance.contactListener = this;
            BindingContext = this;
        }

        private void initGroupUser()
        {
            groupedUsers = new List<UserGroups>();
            groupedUsers.Add(users);
            groupedUsers.Add(contacts);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var users = ContactService.Instance.getUsers(true);
            var contacts = ContactService.Instance.getUsers(false);
            this.updateGroupedUsers(users, contacts);
            ItemsListView.ItemsSource = groupedUsers;
            this.OnPropertyChanged("groupedUsers");

        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            User u = e.Item as User;
            if (u.isPingAvaliable)
            {
                bool isSendPingSuccessful = await Repository.Instance.sendPing(new Ping(FileSystemHelper.Instance.loadCardentials().number, u.number, DateTime.Now.AddMinutes(30).Ticks.ToString()));
                if (isSendPingSuccessful)
                {
                    bool isPushSuccessful = await Repository.Instance.sendPushNotification(FileSystemHelper.Instance.loadCardentials().name, u.token);
                }
                else
                {
                    //TODO
                }
            }
            else
            {
                var message = "i would like to catch up with ya";
                var manager = SmsManager.Default;
                manager.SendTextMessage(u.number, null, message, null, null);
            }

        }

        public void onUserChanged(List<User> users, bool isServerUser)
        {

            this.updateGroupedUsers(isServerUser ? users : null, isServerUser ? null : users);
            ItemsListView.ItemsSource = groupedUsers;
            this.OnPropertyChanged("groupedUsers");
        }

        private void updateGroupedUsers(List<User> users, List<User> contacts)
        {

            if (users != null)
            {
                this.users = new UserGroups("Users (TB)");
                foreach (User u in users)
                {
                    this.users.Add(u);
                }

            }
            if (contacts != null)
            {
                this.contacts = new UserGroups("Contacts (TB)");
                foreach (User u in contacts)
                {
                    this.contacts.Add(u);
                }
            }
            initGroupUser();

        }

        #region INotifyPropertyChanged Implementation

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}