using System;
using System.Collections.Generic;

namespace my_books.Models.ViewModel
{
    // ReSharper disable once InconsistentNaming
    public class BookViewModel
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
        
        //Navigation properties
        public string Publisher { get; set; }
        
    }
    
    public class BookWithAuthorViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; }
        public int? Rate { get; set; }
        public string CoverUrl { get; set; }

        //Navigation properties
        public string PublisherName { get; set; }
        public List<string> AuthorsNames { get; set; }
    }
}
