using System;
using Tasker.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tasker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SetContentAsync();
        }

        private async void SetContentAsync()
        {
            try
            {
                string token = await SecureStorage.GetAsync("userToken");

                if (!string.IsNullOrEmpty(token))
                {
                    MainPage = new NavigationPage(new TasksView());
                }
                else 
                {
                    MainPage = new NavigationPage(new LoginView());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MainPage = new MainPage();

            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
