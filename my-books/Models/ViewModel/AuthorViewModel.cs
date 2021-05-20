using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace my_books.Models.ViewModel
{
    public class AuthorViewModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The name has to contain at least 2 characters")]
        public string Fullname { get; set; }
    }

    public class AuthorWithBooksViewModel
    {
        public string Fullname { get; set; }
        public List<string> BookTitles { get; set; }
    }
}
