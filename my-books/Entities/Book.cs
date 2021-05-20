using System;
using System.Collections.Generic;
using my_books.Models.InputModel;

namespace my_books.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string Author { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public DateTime DateAdded { get; set; }

        //Navigation Properties
        public string Publisher { get; set; }
    }
}