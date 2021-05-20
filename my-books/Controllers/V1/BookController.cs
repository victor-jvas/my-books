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
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        /// <param name="book">Book to be created info</param>
        /// <response code="200">When the book is created successfully</response>
        /// <response code="422">When there is already a book with the same name and publisher</response>
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
        /// <response code="200">Response a book list</response>
        /// <response code="204">When there are no books registered</response>
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
        /// <response code="200">The specified book</response>
        /// <response code="204">When no book with the specified id is found</response>
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

        /// <summary>
        /// Update a existing book
        /// </summary>
        /// <param name="id">The id of the book to be updated</param>
        /// <param name="book">the name of the book</param>
        /// <response code="200">Successful update the book</response>
        /// <response code="404">No book found with corresponding Id</response>
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
        
        /// <summary>
        /// Delete a book
        /// </summary>
        /// <param name="id">The id of the book to be deleted</param>
        /// <response code="200">When the game is deleted</response>
        /// <response code="404">When there is no game with the corresponding Id</response>
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