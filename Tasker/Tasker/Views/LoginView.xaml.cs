using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tasker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private async void NavigateToTasksPage(object sender, EventArgs e)
        {
            try
            {
                string token = await SecureStorage.GetAsync("userToken");
                if (!string.IsNullOrEmpty(token))
                {
                    await Navigation.PushAsync(new TasksView());
                }
                {
                    await DisplayAlert("Unauthorized", "Please, sign in", "OK");
                }
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
            }
        }
    }
}