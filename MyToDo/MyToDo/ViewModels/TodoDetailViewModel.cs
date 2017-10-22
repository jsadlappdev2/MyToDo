using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyToDo.Models;
using System.Collections.Generic;
using System;
using System.Text;
using Plugin.Messaging;
using Plugin.ExternalMaps;

using MyToDo.Services;


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

        public ICommand SendemailCommand { get; set; }
        public ICommand SendTextCommand { get; set; }
        public ICommand PhoneCallCommand { get; set; }

        public ICommand ExternalMapCommand { get; set; }




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

            SendemailCommand  = new Command(SendEmail);

            SendTextCommand = new Command(SendText);

            PhoneCallCommand = new Command(PhoneCall);


            //_CapabilityService = DependencyService.Get<ICapabilityService>();


            ExternalMapCommand = new Command(ExternalMap);


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


        //public ICapabilityService CapabilityService => _CapabilityService;


        ////Send email
        //Command _EmailCommand;

        //public Command EmailCommand => _EmailCommand ??
        //                               (_EmailCommand = new Command(SendEmail));

        //void ExecuteEmailCommandCommand()
        //{
        //    if (string.IsNullOrWhiteSpace(IsDoneLists[11].ToString()))
        //        return;

        //    if (CapabilityService.CanSendEmail)
        //    {

        //        var emailTask = MessagingPlugin.EmailMessenger;
        //        if (emailTask.CanSendEmail)
        //            emailTask.SendEmail(IsDoneLists[11].ToString());
        //    }

        //    else
        //    {
        //        //MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
        //        //{
        //        //    Title = "Simulator Not Supported",
        //        //    Message = "Email composition is not supported in the iOS simulator.",
        //        //    Cancel = "OK"
        //        //});
        //    }
        //}


        void SendEmail()
        {
            var emailTask = MessagingPlugin.EmailMessenger;
            if (emailTask.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, CC, or BCC.
                emailTask.SendEmail("shenjr81@gmail.com", "Messaging Plugin Test", "Hello from your friends at Xamarin!");

                // Send a more complex email with the EmailMessageBuilder fluent interface.
                //var email = new EmailMessageBuilder()
                //  .To("plugins@xamarin.com")
                //  .Cc("plugins.cc@xamarin.com")
                //  .Bcc(new[] { "plugins.bcc@xamarin.com", "plugins.bcc2@xamarin.com" })
                //  .Subject("Xamarin Messaging Plugin")
                //  .Body("Hello from your friends at Xamarin!")
                //  .Build();

                //emailTask.SendEmail(email);


            }
        }


            void SendText()
            {
                var smsMessenger = MessagingPlugin.SmsMessenger;
                if (smsMessenger.CanSendSms)
                    smsMessenger.SendSms("0499116148", "Hello from JS!");     
                
             


            }


            void PhoneCall()
                {

           

                //if (string.IsNullOrWhiteSpace(IsDoneLists[0].ToString()))
                //return;

            // Make Phone Call
            var phoneCallTask = MessagingPlugin.PhoneDialer;
                if (phoneCallTask.CanMakePhoneCall)
                    phoneCallTask.MakePhoneCall(IsDoneLists[3].ToString());


            }


       async void ExternalMap()
        {
            try
            {
                var success = await CrossExternalMaps.Current.NavigateTo("Another Home", "33 Devereux Road", "Linden Park", "SA", "5065", "AU", "AU");
                //msg.Text = success.ToString();
            }
            catch (Exception ee)
            {
                await DisplayAlert("Alert", "Error: " + ee.Message.ToString(), "OK");

            }

        }

        private Task DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }
    }
}
