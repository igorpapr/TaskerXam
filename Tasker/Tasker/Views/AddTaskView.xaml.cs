using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tasker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tasker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTaskView : ContentPage
    {
        AddTaskViewModel addTaskViewModel;

        public AddTaskView(ICommand refreshList)
        {
            InitializeComponent();
            addTaskViewModel = new AddTaskViewModel(refreshList);
            BindingContext = addTaskViewModel;
        }

        private void AddTask(object sender, EventArgs e)
        {
            if (addTaskViewModel.AddCommand.CanExecute(null))
            {
                addTaskViewModel.AddCommand.Execute(null);
            }
            
        }
    }
}