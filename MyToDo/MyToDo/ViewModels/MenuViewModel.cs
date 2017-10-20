using System.Collections.Generic;
using Xamarin.Forms;
using MyToDo.Models;
using MyToDo.Views;

namespace MyToDo.ViewModels
{
    public class MenuViewModel: BaseViewModel
    {
        public List<MenuItem> menuitems { get; set; }

        public MenuViewModel()
        {


            menuitems = new List<MenuItem>();

            menuitems.Add(new MenuItem
            {
                Title = "Todo",
                IconSource = "contacts.png",
                TargetType = typeof(TodoPage)
            });

            menuitems.Add(new MenuItem
            {
                Title = "IsDone",
                IconSource = "contacts.png",
                TargetType = typeof(TodoIsdonePage)
            });
            menuitems.Add(new MenuItem
            {
                Title = "User",
                IconSource = "todo.png",
                TargetType = typeof(UserPage)
            });
            menuitems.Add(new MenuItem
            {
                Title = "Person",
                IconSource = "reminders.png",
                TargetType = typeof(PersonPage)
            });


            menuitems.Add(new MenuItem
            {
                Title = "IsDone_NEW",
                IconSource = "reminders.png",
                TargetType = typeof(IsdonePage)
            });



        }

    }
}
