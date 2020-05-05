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
    public class TasksViewModel : INotifyPropertyChanged
    {
        private readonly ConnectionService api = new ConnectionService();
        private List<MyTask> _tasks;

        public List<MyTask> Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetTasksCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        string token = await SecureStorage.GetAsync("userToken");
                        Tasks = await api.GetTasks(token);
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
