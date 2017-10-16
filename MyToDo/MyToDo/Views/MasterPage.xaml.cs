﻿using System.Collections.Generic;
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
                Title = "Todo",
                IconSource = "contacts.png",
                TargetType = typeof(TodoPage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "User",
                IconSource = "todo.png",
                TargetType = typeof(UserPage)
            });
            masterPageItems.Add(new MasterPageItem
            {
                Title = "Todo2",
                IconSource = "reminders.png",
                TargetType = typeof(TodoPage)
            });

            listView.ItemsSource = masterPageItems;
        }
    }
}