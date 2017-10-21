using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using MyToDo.Models;
using System.Collections.Generic;
using System;
using Plugin.Messaging;


namespace MyToDo.ViewModels
{
    public class TodoDetailViewModel : BaseViewModel
    {
        readonly Database database;
        public ObservableCollection<string> Records { get; set; }
        public List<Todo> IsDoneLists { get; set; }
        public ICommand IsDoneFilterCommand { get; set; }
        public ICommand UpdateIsDoneCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public TodoDetailViewModel()
        {

            database = new Database("MyTodoDB");
            database.CreateTable<Todo>();
            IsDoneLists = new List<Todo>();

            //manual add data
            //IsDoneLists.Add(new Todo() {Description="Test1" });
            //IsDoneLists.Add(new Todo() { Description = "Test2" });
            //IsDoneLists.Add(new Todo() { Description = "Test2" });

            //Get IsDone records
            FilterByIsDone("Yes");


            IsDoneFilterCommand = new Command(FilterByIsDone);
            UpdateIsDoneCommand = new Command(DoneUpdate);
            DeleteCommand = new Command(Delete);


        }


        void Delete(object obj)
        {
            var itemString = (string)obj;
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            int id;
            if (int.TryParse(columns[0], out id))
            {
                database.DeleteItem<Todo>(id);

            }
        }


        void DoneUpdate(object obj)
        {
            var itemString = (string)obj;
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            int id;
            if (int.TryParse(columns[0], out id))
            {
                database.UpdateItem("update Todo set IsDone='Yes' where id=?", id);

                //  RaisePropertyChanged(nameof(Records));

            }
        }


        void FilterByIsDone(object obj)
        {
            IsDoneLists.Clear();

            var isdone = ((string)obj) == "Yes" ? "Yes" : "No";

            var result = database.Query<Todo>("SELECT * FROM Todo WHERE ID =16 and IsDone = ? order by ID asc", new object[] { isdone });

            IsDoneLists = result.ToList();

        }
        void ShowAllRecords()
        {

            IsDoneLists.Clear();
            var todos = database.GetItems<Todo>();
            IsDoneLists = todos.ToList();


        }

      
        //Send email
        Command _EmailCommand;

        public Command EmailCommand => _EmailCommand ??
                                       (_EmailCommand = new Command(ExecuteEmailCommandCommand));

        void ExecuteEmailCommandCommand()
        {
            if (string.IsNullOrWhiteSpace(IsDoneLists[11].ToString()))
                return;


            var emailTask = MessagingPlugin.EmailMessenger;
                if (emailTask.CanSendEmail)
                    emailTask.SendEmail(IsDoneLists[11].ToString());
          
        }



    }
}
