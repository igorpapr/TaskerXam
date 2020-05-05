using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Models;
using Tasker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tasker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskDetailsView : ContentPage
    {
        public TaskDetailsView(MyTask task)
        {
            InitializeComponent();

            TaskDetailsViewModel taskDetailsViewModel = new TaskDetailsViewModel
            {
                Item = task
            };
            BindingContext = taskDetailsViewModel;
        }

        private async void PreviousScreen(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}