using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using my_books.Models;

namespace my_books.Data.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Fullname { get; set; }

        //Navigation properties

        public List<BookAuthor> BookAuthors { get; set; }
    }
}
