using System;
using Tasker.Models;
using Tasker.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tasker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TasksView : ContentPage
    {
        TasksViewModel tasksViewModel;

        public TasksView()
        {
            InitializeComponent();
            tasksViewModel = new TasksViewModel();
            BindingContext = tasksViewModel;
            if (tasksViewModel.GetTasksCommand.CanExecute(null))
            {
                tasksViewModel.GetTasksCommand.Execute(null);
            }
        }

        private async void AddNewTask(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddTaskView(tasksViewModel.GetTasksCommand));
        }

        private async void ShowDetails(object sender, ItemTappedEventArgs e)
        {
            var idea = e.Item as MyTask;
            await Navigation.PushAsync(new TaskDetailsView(idea));
        }

        private void LogOut(object sender, EventArgs e)
        {                    
            try
            {
                SecureStorage.Remove("userToken");
                Application.Current.MainPage = new NavigationPage(new LoginView());
            }
            catch (Exception e1)
            {
                Console.WriteLine(e1.Message);
            }
         
        }
    }
}