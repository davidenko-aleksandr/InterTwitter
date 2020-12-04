using System;
using System.Collections.Generic;
using InterTwitter.Enums;

namespace InterTwitter.Models
{
    public class OwlModel
    {
        public int Id { get; set; }

        public UserModel Author { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public List<string> Media { get; set; }

        public OwlType MediaType { get; set; }

        public List<int> LikesList { get; set; }

        public List<int> SavesList { get; set; }
    }
}