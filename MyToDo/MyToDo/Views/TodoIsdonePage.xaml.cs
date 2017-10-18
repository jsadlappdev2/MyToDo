using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MyToDo.ViewModels;

namespace MyToDo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoIsdonePage : ContentPage
    {
        //Todo_DoneViewModel viewModel;

        public TodoIsdonePage()
        {

           BindingContext = new IsdoneViewModel();

            InitializeComponent();

            //viewModel = new Todo_DoneViewModel();


            //try this added by JS

            //ToolbarItems.Add(new ToolbarItem
            //{
            //    StyleId = "Settings",
            //    Icon = "ic_action_settings.png",
            //    Text = "Settings",
            //    Order = ToolbarItemOrder.Primary,
            //    Command = viewModel.UpdateIsDoneCommand
            //});

            //ToolbarItems.Add(new ToolbarItem
            //{
            //    Icon = "ic_action_navigation_arrow_forward.png",
            //    Text = "Already Logged In",
            //    Order = ToolbarItemOrder.Primary,
            //    Command = viewModel.DeleteCommand
            //});

        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    if (BindingContext == null)
        //    {
        //        BindingContext = viewModel;
        //        viewModel.IsDoneFilterCommand.Execute(null);
        //    }
        //}

    }
}