using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Tasker.Models;
using Tasker.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Tasker.ViewModels
{
    class AddTaskViewModel : INotifyPropertyChanged
    {

        ConnectionService api = new ConnectionService();
        ICommand refreshBack;

        public AddTaskViewModel(ICommand refreshBack)
        {
            this.refreshBack = refreshBack;
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public ICommand AddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        string token = await SecureStorage.GetAsync("userToken");

                        var task = new MyTask
                        {
                            Title = Title,
                            Description = Description,
                        };
                        var res = await api.AddTask(task, token);
                        if (res)
                        {
                            if (refreshBack.CanExecute(null))
                            {
                                refreshBack.Execute(null);
                            }
                            await Application.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Couldn't add new task", "Something went wrong. You can try again.","OK");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
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
