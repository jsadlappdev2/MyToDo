﻿using System;

namespace MyToDo.Models
{
  public  class MasterPageItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }

        public string NewItemCount { get; set; }
    }
}
