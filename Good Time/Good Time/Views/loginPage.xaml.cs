using Good_Time.Models;
using Good_Time.Services;
using Good_Time.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Good_Time.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class loginPage : ContentPage
    {
        public loginPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<loginPage>(this, "Hi", (sender) =>
            {
                // do something whenever the "Hi" message is sent
            });
        }

        private void sendRegistrationMessage()
        {
            MessagingCenter.Subscribe<loginPage>(this, "Hi", (sender) =>
            {
                // do something whenever the "Hi" message is sent
            });
        }

        private void getRegistrationToken(object sender, EventArgs e)
        {
            if (
                userNumber.Text != null &&
                userNumber.Text.Length > 0 
                )
            {
                LoginService.Instance.getToken(userNumber.Text);
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (userName.Text != null &&
                userName.Text.Length > 0 &&
                userNumber.Text != null &&
                userNumber.Text.Length > 0 &&
                registrationToken.Text != null &&
                registrationToken.Text.Length > 0)
            {
                User u = new User(userName.Text, userNumber.Text, "", Repository.Instance.token);
                bool isRegisterd = await LoginService.Instance.registerAsync(registrationToken.Text, u);
                if (isRegisterd)
                {
                    ((App)App.Current).MainPage = new TabPage();
                }
                else
                {
                    registration_status.Text = "Error!!!";
                }
            }
            else
            {
                registration_status.Text = "Invalid Cerdentials!!!";
            }
        }

    }
}