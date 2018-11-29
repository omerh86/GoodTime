using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Good_Time.Views;
using Good_Time.Util;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Good_Time
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();


            if (FileSystemHelper.Instance.loadCardentials() != null)
            {
                MainPage = new NavigationPage(new TabPage());
            }
            else
            {
               // MainPage = new NavigationPage(new TabPage());
                MainPage = new NavigationPage(new loginPage());
            }

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
