using System;
using System.Collections.Generic;

namespace InterTwitter.Models
{
    public class OwlModel
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public List<string> Media { get; set; }
    }
}