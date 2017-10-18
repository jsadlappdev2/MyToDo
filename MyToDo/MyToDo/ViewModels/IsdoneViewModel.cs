using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using MyToDo.Models;
using System.Collections.Generic;
using System;

namespace MyToDo.ViewModels
{
    public class IsdoneViewModel: BaseViewModel
    {
        readonly Database database;

        public string Type { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public string URL { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public string DueDate { get; set; }
        public string IsDone { get; set; }
              

        public ObservableCollection<string> Records { get; set; }
        //here is important noted by JS.
        public List<Todo> IsDoneLists { get; set; }
        //

        public ICommand IsDoneFilterCommand { get; set; }
        public ICommand UpdateIsDoneCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public IsdoneViewModel()
        {
           
            database = new Database("MyTodoDB");
            database.CreateTable<Todo>();
            Records = new ObservableCollection<string>();
            IsDoneLists = new List<Todo>();

            //manual add data
            //IsDoneLists.Add(new Todo() {Description="Test1" });
            //IsDoneLists.Add(new Todo() { Description = "Test2" });
            //IsDoneLists.Add(new Todo() { Description = "Test2" });

            IsDoneFilterCommand = new Command(FilterByIsDone);
            UpdateIsDoneCommand = new Command(DoneUpdate);
            DeleteCommand = new Command(Delete);
            ShowAllRecords();

        }


        void Delete(object obj)
        {
            var itemString = (string)obj;
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            int id;
            if (int.TryParse(columns[0], out id))
            {
                database.DeleteItem<Todo>(id);
                Records.Remove((string)obj);
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

                RaisePropertyChanged(nameof(Records));
              
            }
        }


        void FilterByIsDone(object obj)
        {
            IsDoneLists.Clear();

            var isdone = ((string)obj) == "Yes" ? "Yes" : "No";
            //looks result is <List> type.
            var result = database.Query<Todo>("SELECT * FROM Todo WHERE IsDone = 'Yes' order by ID asc", new object[] { isdone });

            IsDoneLists=result.ToList();

            //no longer using.
            //Records.Clear();
            //foreach (var todoisdone in result)
            //{
            //    Records.Add(todoisdone.ToString());
            //}
        }
        void ShowAllRecords()
        {

            IsDoneLists.Clear();
           // Records.Clear();
            var todos = database.GetItems<Todo>();

            IsDoneLists = todos.ToList();
            //foreach (var todo in todos)
            //{
            //    // Records.Add(todo.ToString());
            //    IsDoneLists.Add(todo);
            //}

        }




    }
}
