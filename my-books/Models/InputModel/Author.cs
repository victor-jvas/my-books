using System.Collections.Generic;

namespace my_books.Models.InputModel
{
    public class Author
    {
        public int Id { get; set; }
        public string Fullname { get; set; }

        //Navigation properties

        public List<BookAuthor> BookAuthors { get; set; }
    }
}
