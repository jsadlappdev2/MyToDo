using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using MyToDo.Models;

namespace MyToDo.ViewModels
{
  public  class TodoViewModel:BaseViewModel
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
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeleteAllCommand { get; set; }
        public ICommand GenderCommand { get; set; }
        public ICommand AgeFilterCommand { get; set; }

        //added by JS for testing
        public ICommand DoneCommand { get; set; }

        public ICommand IsDoneFilterCommand { get; set; }
        public ICommand NotDoneFilterCommand { get; set; }

        public TodoViewModel()
        {
            AddCommand = new Command(Add);
            DeleteCommand = new Command(Delete);
            DeleteAllCommand = new Command(DeleteAll);
            DoneCommand = new Command(DoneUpdate);
            GenderCommand = new Command(FilterByGender);
            AgeFilterCommand = new Command(FilterByAge);
            database = new Database("MyTodoDB");
            database.CreateTable<Todo>();
            Records = new ObservableCollection<string>();
            ShowAllRecords();
            //added by JS
            IsDoneFilterCommand = new Command(FilterByIsDone);
            NotDoneFilterCommand = new Command(FilterByNotDone);
        }

        void Add()
        {

            var record = new Todo
            {

                Type = Type,
                Description = Description,
                Telephone = Telephone,
                URL = URL,
                Address = Address,
                Photo = "", //set as default picture and change it later.,
                                     //  DueDate = dpDueDate.Date.ToString("d"),
                DueDate = DueDate,
                IsDone = IsDone

            };
            database.SaveItem(record);

            Records.Add(record.ToString());
            RaisePropertyChanged(nameof(Records));
            ClearForm();

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

        //JS: added by JS and need to test.
        void DoneUpdate(object obj)
        {
            var itemString = (string)obj;
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            int id;
            if (int.TryParse(columns[0], out id))
            {
                database.UpdateItem<Todo>(id);
                // Records.Remove((string)obj);
                RaisePropertyChanged(nameof(Records));
                ClearForm();
            }
        }

        //Added by JS--------------------------------------------------------------------------------------
        void FilterByIsDone()
        {
            var result = database.QueryNoArgs<Todo>("SELECT * FROM TodoList WHERE IdDone = 'Yes' ");
            Records.Clear();
            foreach (var todo in result)
            {
                Records.Add(todo.ToString());
            }
        }

        void FilterByNotDone()
        {
            var result = database.QueryNoArgs<Todo>("SELECT * FROM TodoList WHERE IdDone = 'No' ");
            Records.Clear();
            foreach (var todo in result)
            {
                Records.Add(todo.ToString());
            }
        }

        //-----------------------------------------------------------------------------------------------------

        void DeleteAll()
        {
            database.DeleteAll<Todo>();
            Records.Clear();
        }

        void FilterByGender(object obj)
        {
            var gender = ((string)obj) == "Female" ? Gender.Female : Gender.Male;
            var result = database.Query<Todo>("SELECT * FROM TodoList WHERE Gender = ?", new object[] { gender });
            Records.Clear();
            foreach (var person in result)
            {
                Records.Add(person.ToString());
            }
        }

        void FilterByAge(object obj)
        {
            int age;
            if (int.TryParse((string)obj, out age))
            {
                var result = database.Query<Todo>("SELECT * FROM TodoList WHERE Age > ?", new object[] { age });
                Records.Clear();
                foreach (var person in result)
                {
                    Records.Add(person.ToString());
                }
            }
        }

        void ClearForm()
        {
            Type = string.Empty;
            Description = string.Empty;
            Telephone = string.Empty;
            URL = string.Empty;
            Address = string.Empty;
            Photo = string.Empty;
            DueDate = string.Empty;
            IsDone = string.Empty;

            RaisePropertyChanged(nameof(Type));
            RaisePropertyChanged(nameof(Description));
            RaisePropertyChanged(nameof(Telephone));
            RaisePropertyChanged(nameof(URL));
            RaisePropertyChanged(nameof(Address));
            RaisePropertyChanged(nameof(Photo));
            RaisePropertyChanged(nameof(DueDate));
            RaisePropertyChanged(nameof(IsDone));
        }

        void ShowAllRecords()
        {
            Records.Clear();
            var todos = database.GetItems<Todo>();
            foreach (var todo in todos)
            {
                Records.Add(todo.ToString());
            }
        }
    }
}
