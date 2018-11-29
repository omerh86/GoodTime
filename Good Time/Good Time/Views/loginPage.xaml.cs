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
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (userName.Text != null &&
                userName.Text.Length > 0 &&
                userNumber.Text != null &&
                userNumber.Text.Length > 0 &&
                Repository.Instance.token != null &&
                Repository.Instance.token.Length > 0)
            {
                User u = new User(userName.Text, userNumber.Text, "", Repository.Instance.token);
                bool isUploaded = await Repository.Instance.uploadUserAsync(u);
                if (isUploaded)
                {
                    FileSystemHelper.Instance.saveCardentials(u);
                    ((App)App.Current).MainPage = new TabPage();

                }
            }
        }

    }
}