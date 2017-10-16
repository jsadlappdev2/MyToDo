using System;

namespace MyToDo.Models
{
   
    public class User:BaseItem
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string NickName { get; set; }
        public Gender Gender { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Image_Url { get; set; }


        public override string ToString()
        {
            return $"{ID},{UserId}, {FirstName}, {LastName}, {NickName}, {Gender.ToString()},{Age.ToString()}, {Email},{Password}, {Image_Url}";
        }

    }
}
