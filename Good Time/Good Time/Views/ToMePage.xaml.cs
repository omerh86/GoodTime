using Good_Time.Models;
using Good_Time.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Good_Time.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToMePage : ContentPage
    {
        public ToMePage()
        {
            InitializeComponent();
            bla.Clicked += async (sender, e) =>
            {
             var pings=   await Repository.Instance.getPings();
            };
        }

        private void onAddUser(object sender, ItemTappedEventArgs e)
        {
            // Repository.Instance.uploadUserAsync(new User("omer", "055", "blabla"));
        }


    }
}