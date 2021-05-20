using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_books.Exceptions;
using my_books.Models.InputModel;
using my_books.Models.ViewModel;
using my_books.Services;

namespace my_books.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        /// <param name="book">Book to be created info</param>
        /// <returns code="200">When the book is created successfully</returns>
        /// <returns code="422">When there is already a book with the same name and publisher</returns>
        [HttpPost]
        public async Task<ActionResult<BookViewModel>> AddBook([FromBody] BookInputModel book)
        {

            try
            {
                await _bookService.AddBook(book);
                return Ok(book);
            }
            catch (BookAlreadyExistException exception)
            {
                return UnprocessableEntity("There is already a book with this name for this publisher");
            }
        }

        /// <summary>
        /// Return all the books using pagination
        /// </summary>
        /// <param name="page">The page number to get the books</param>
        /// <param name="perPage">The number of book per page</param>
        /// <returns code="200">Returns a book list</returns>
        /// <returns code="204">When there are no books registered</returns>
        [HttpGet]
        public async Task<ActionResult<List<BookViewModel>>> GetBooks([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int perPage = 5)
        {
            var books = await _bookService.GetBooks(page, perPage);

            if (!books.Any()) return NoContent();
            
            return Ok(books);
        }
        
        /// <summary>
        /// Returns a book specified by it`s ID
        /// </summary>
        /// <param name="id">The Id of the book to be returned</param>
        /// <returns code="200">The specified book</returns>
        /// <returns code="204">When no book with the specified id is found</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookViewModel>> GetBookById([FromRoute] int id)
        {
            var book = await _bookService.GetBookById(id);

            if (book == null)
            {
                return NoContent();
            }
            return Ok(book);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateBookById([FromRoute] int id, [FromBody] BookInputModel book)
        {
            try
            {
                await _bookService.UpdateBookById(id, book);
                return Ok();
            }
            catch (BookNotRegisteredException exception)
            {
                return NotFound("The book does not exist");
            }
        }
        
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBookById(int id)
        {
            try
            {
                await _bookService.DeleteBookById(id);
                return Ok();
            }
            catch (BookNotRegisteredException exception)
            {
                return NotFound("The book does not exist");
            }
        }
       
    }
}