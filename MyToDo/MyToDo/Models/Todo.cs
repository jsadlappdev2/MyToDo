using System;

namespace MyToDo.Models
{
   public class Todo:BaseItem
    {
        public string Type { get; set; }
        public string Description { get; set; }

        public string Telephone { get; set; }
        public string URL { get; set; }
        public string Address { get; set; }

        public string Photo { get; set; }

        public string DueDate { get; set; }

        public string IsDone { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Type}, {Description}, {Telephone}, {URL},{Address},{Photo},{DueDate},{IsDone}";
        }

    }
}
