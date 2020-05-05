using System;
using System.Windows.Input;
using Tasker.Services;
using Xamarin.Forms;
using Xamarin.Essentials;
using Tasker.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Tasker.Views;
using System.Threading.Tasks;

namespace Tasker.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly ConnectionService api = new ConnectionService();

        public string Username { get; set; }
        public string Password { get; set; }


        public LoginViewModel()
        {}

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    
                    var user = new UserLoginData
                    {
                        Username = Username,
                        Password = Password
                    };

                    string token = await api.Login(user);

                    if (string.IsNullOrEmpty(token))
                    {
                        await Application.Current.MainPage.DisplayAlert("Couldn't authorize", "Please, try again", "OK");
                    }
                    else
                    {
                        try
                        {
                            await SecureStorage.SetAsync("userToken", token);
                            Application.Current.MainPage = new NavigationPage(new TasksView());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
