using System;
using System.Collections.Generic;

namespace MyToDo.Models
{
   public class Todo:BaseItem
    {
        public string Type { get; set; }
        public string Description { get; set; }

        public string Telephone { get; set; }

        public bool HasPhone { get; set; }
        public string URL { get; set; }

        public bool HasUrl { get; set; }
        public string Address { get; set; }

        public bool HasAddress { get; set; }

        public string Photo { get; set; }

        public bool HasPhoto { get; set; }

        public string Email { get; set; }

        public bool  HasEmail { get; set; }

        public string DueDate { get; set; }

        public string IsDone { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Type}, {Description}, {Telephone}, {URL},{Address},{Photo},{DueDate},{IsDone}";
        }

        public static implicit operator List<object>(Todo v)
        {
            throw new NotImplementedException();
        }
    }
}
