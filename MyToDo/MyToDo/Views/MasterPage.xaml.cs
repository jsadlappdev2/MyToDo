using System.Collections.Generic;
using Xamarin.Forms;
using MyToDo.Models;


namespace MyToDo.Views
{
   
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } }
        public MasterPage()
        {
            InitializeComponent();
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem
            {
                Title = "IsDone_new",
                IconSource = "contacts.png",
                TargetType = typeof(IsdonePage),
                NewItemCount ="99+ new"
            });

            masterPageItems.Add(new MasterPageItem
            {
                Title = "IsDone",
                IconSource = "contacts.png",
                TargetType = typeof(TodoIsdonePage),
                NewItemCount = "15 new"
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "User",
                IconSource = "todo.png",
                TargetType = typeof(UserPage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Person",
                IconSource = "reminders.png",
                TargetType = typeof(PersonPage)
            });


            masterPageItems.Add(new MasterPageItem
            {
                Title = "New Todos",
                IconSource = "reminders.png",
                TargetType = typeof(TodoPage)
            });


            masterPageItems.Add(new MasterPageItem
            {
                Title = "Todo Details",
                IconSource = "reminders.png",
                TargetType = typeof(TodoDetailPage)
            });

            listView.ItemsSource = masterPageItems;
        }
    }
}