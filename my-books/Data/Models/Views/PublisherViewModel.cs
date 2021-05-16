using System.Collections.Generic;
using my_books.Data.Models.Views;

namespace my_books.Models.View
{
    public class PublisherViewModel
    {
        public string Name { get; set; }
    }

    public class PublisherAllInfoViewModel
    {
        public string Name { get; set; }

        public List<BookAuthorViewModel> BookAuthors { get; set; }
    }

    public class BookAuthorViewModel
    {
        public string BookName { get; set; }
        public List<string> BookAuthors { get; set; }
    }
}