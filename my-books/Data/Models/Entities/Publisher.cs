using System.Collections.Generic;

namespace my_books.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Navigation Properties - Properties used to define the relationship between models
        public List<Book> Books { get; set; }
    }
}