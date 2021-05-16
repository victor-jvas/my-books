using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace my_books.Data.Models.Views
{
    public class AuthorViewModel
    {
        public string Fullname { get; set; }
    }

    public class AuthorWithBooksViewModel
    {
        public string Fullname { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
