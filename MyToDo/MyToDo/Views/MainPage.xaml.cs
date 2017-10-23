using System;
using Xamarin.Forms;
using MyToDo.ViewModels;
using MyToDo.Models;
using MyToDo.Views;

namespace MyToDo
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            masterPage.ListView.ItemSelected += OnItemSelected;

            //if (Device.RuntimePlatform == Device.Windows)
            //{
            //    MasterBehavior = MasterBehavior.Popover;
            //}
        }
        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType)) { BarBackgroundColor= Color.FromHex("#547799") };
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
