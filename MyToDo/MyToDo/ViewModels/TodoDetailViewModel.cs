using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using MyToDo.Models;
using System.Collections.Generic;
using System;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Xamarin.Forms.Maps;
using System.Collections.Generic;

namespace MyToDo.ViewModels
{
    public class TodoDetailViewModel: BaseViewModel
    {
        public TodoDetailViewModel(Todo todo, int id)
        {

            _CapabilityService = DependencyService.Get<ICapabilityService>();

            _Geocoder = new Geocoder();

            Todo = new List<Todo>();

            database = new Database("MyTodoDB");
            database.CreateTable<Todo>();




        }

        public List<Todo> Todo { private set; get; }

        readonly Database database;    


        public bool HasAddress => !string.IsNullOrWhiteSpace(Todo?.Address);

        // this is just a utility service that we're using in this demo app to mitigate some limitations of the iOS simulator
        readonly ICapabilityService _CapabilityService;
        readonly Geocoder _Geocoder;

        public ICommand IsDoneFilterCommand { get; set; }
        public ICommand UpdateIsDoneCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        //public TodoDetailViewModel(int id, Todo todo)
        //{
           
        //    database = new Database("MyTodoDB");
        //    database.CreateTable<Todo>();
        //    //     todo = new List<Todo>();
        //    Todo = todo;
        //    _CapabilityService = DependencyService.Get<ICapabilityService>();

        //    _Geocoder = new Geocoder();

        //    //manual add data
        //    //IsDoneLists.Add(new Todo() {Description="Test1" });
        //    //IsDoneLists.Add(new Todo() { Description = "Test2" });
        //    //IsDoneLists.Add(new Todo() { Description = "Test2" });

        //    //Get IsDone records
        //    FilterByID(id);
           

        //    //IsDoneFilterCommand = new Command(FilterByIsDone);
        //    //UpdateIsDoneCommand = new Command(DoneUpdate);
        //    //DeleteCommand = new Command(Delete);
           

        //}


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


        void FilterByID(int id)
        {
            tododetail.Clear();          
         
            var result = database.Query<Todo>("SELECT * FROM Todo WHERE ID = ? ", new object[] { id });

            tododetail = result.ToList();

        }



        Command _GetDirectionsCommand;

        public Command GetDirectionsCommand
        {
            get
            {
                return _GetDirectionsCommand ??
                (_GetDirectionsCommand = new Command(async () =>
                        await ExecuteGetDirectionsCommand()));
            }
        }

        public ICapabilityService CapabilityService => _CapabilityService;

        async Task ExecuteGetDirectionsCommand()
        {
            var position = await GetPosition();

            var pin = new Pin() { Position = position };

            await CrossExternalMaps.Current.NavigateTo(pin.Label, pin.Position.Latitude, pin.Position.Longitude, NavigationType.Driving);
        }

        public void SetupMap()
        {
            if (HasAddress)
            {
                MessagingService.Current.SendMessage(MessageKeys.SetupMap);
            }
        }

        public void DisplayGeocodingError()
        {
            //MessagingService.Current.SendMessage<MessagingServiceAlert>(MessageKeys.DisplayAlert, new MessagingServiceAlert()
            //    {
            //        Title = "Geocoding Error", 
            //        Message = "Please make sure the address is valid, or that you have a network connection.",
            //        Cancel = "OK"
            //    });

            IsBusy = false;
        }

        public async Task<Position> GetPosition()
        {
            if (!HasAddress)
                return new Position(0, 0);

            IsBusy = true;

            Position p;

            p = (await _Geocoder.GetPositionsForAddressAsync(Acquaintance.AddressString)).FirstOrDefault();

            // The Android geocoder (the underlying implementation in Android itself) fails with some addresses unless they're rounded to the hundreds.
            // So, this deals with that edge case.
            if (p.Latitude == 0 && p.Longitude == 0 && AddressBeginsWithNumber(Acquaintance.AddressString) && Device.OS == TargetPlatform.Android)
            {
                var roundedAddress = GetAddressWithRoundedStreetNumber(Acquaintance.AddressString);

                p = (await _Geocoder.GetPositionsForAddressAsync(roundedAddress)).FirstOrDefault();
            }

            IsBusy = false;

            return p;
        }




    }
}
