using System.Collections.Generic;
using my_books.Entities;

namespace my_books.Models.InputModel
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation Properties - Properties used to define the relationship between models
        public List<Book> Books { get; set; }
    }
}