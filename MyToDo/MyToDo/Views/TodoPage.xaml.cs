﻿using System;
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
    public partial class TodoPage : ContentPage
    {
        public TodoPage()
        {
            BindingContext = new TodoViewModel();
           InitializeComponent();
        }
    }
}

