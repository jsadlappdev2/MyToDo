using System;

namespace MyToDo.Models
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Person:BaseItem
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public Gender Gender { get; set; }
        public override string ToString()
        {
            return $"{ID}, {FirstName}, {LastName}, {Age}, {Country}, {Gender.ToString()}";
        }
    }
}
