using Microsoft.AspNetCore.Mvc;
using my_books.Data.Models.Views;
using my_books.Models.View;
using my_books.Services;

namespace my_books.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }


        [HttpPost]
        public IActionResult AddBook([FromBody]BookViewModel book)
        {
            _bookService.AddBook(book);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            var books = _bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            return Ok(book);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookViewModel book)
        {
            var updatedBook = _bookService.UpdateBookById(id, book);
            return Ok(updatedBook);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBookById(int id)
        {
            _bookService.DeleteBookById(id);
            return Ok();
        }
       
    }
}