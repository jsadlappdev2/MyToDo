using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using MyToDo.Models;

namespace MyToDo.ViewModels
{
   public class UserViewModel: BaseViewModel
    {
        readonly Database database;

        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Image_Url { get; set; }
        public string Age { get; set; }
        public int GenderIndex { get; set; } = -1;
        public ObservableCollection<string> Records { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeleteAllCommand { get; set; }
        public ICommand GenderCommand { get; set; }
        public ICommand AgeFilterCommand { get; set; }

        public UserViewModel()
        {
            AddCommand = new Command(Add);
            DeleteCommand = new Command(Delete);
            DeleteAllCommand = new Command(DeleteAll);
            GenderCommand = new Command(FilterByGender);
            AgeFilterCommand = new Command(FilterByAge);
            database = new Database("MyTodoDB");
            database.CreateTable<User>();
            Records = new ObservableCollection<string>();
            ShowAllRecords();
        }

        void Add()
        {
            int age;
            if (int.TryParse(Age, out age))
            {
                var record = new User
                {
                    UserId = UserId,
                    FirstName = FirstName,
                    LastName = LastName,
                    NickName = NickName,
                    Gender = GenderIndex == 0 ? Gender.Male : Gender.Female,
                    Age = age,
                    Email = Email,
                    Password = Password,
                    Image_Url = "devnl.png" //set as default picture and change it later.


                };
                database.SaveItem(record);

                Records.Add(record.ToString());
                RaisePropertyChanged(nameof(Records));
                ClearForm();
            }
        }

        void Delete(object obj)
        {
            var itemString = (string)obj;
            var columns = itemString.Split(',').Select(i => i.Trim()).ToList();
            int id;
            if (int.TryParse(columns[0], out id))
            {
                database.DeleteItem<User>(id);
                Records.Remove((string)obj);
            }
        }

        void DeleteAll()
        {
            database.DeleteAll<User>();
            Records.Clear();
        }

        void FilterByGender(object obj)
        {
            var gender = ((string)obj) == "Female" ? Gender.Female : Gender.Male;
            var result = database.Query<User>("SELECT * FROM User WHERE Gender = ?", new object[] { gender });
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
                var result = database.Query<User>("SELECT * FROM User WHERE Age > ?", new object[] { age });
                Records.Clear();
                foreach (var person in result)
                {
                    Records.Add(person.ToString());
                }
            }
        }

        void ClearForm()
        {
            UserId = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            NickName = string.Empty;
            GenderIndex = -1;
            Age = string.Empty;
            Email = string.Empty;
            Password = string.Empty;

            RaisePropertyChanged(nameof(UserId));
            RaisePropertyChanged(nameof(FirstName));
            RaisePropertyChanged(nameof(LastName));
            RaisePropertyChanged(nameof(NickName));
            RaisePropertyChanged(nameof(Age));
            RaisePropertyChanged(nameof(GenderIndex));
            RaisePropertyChanged(nameof(Email));
            RaisePropertyChanged(nameof(Password));
        }

        void ShowAllRecords()
        {
            Records.Clear();
            var users = database.GetItems<User>();
            foreach (var user in users)
            {
                Records.Add(user.ToString());
            }
        }
    }
}
