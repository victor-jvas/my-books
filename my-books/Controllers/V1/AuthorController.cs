using System;
using Microsoft.AspNetCore.Mvc;
using my_books.Models.ViewModel;
using my_books.Services;

namespace my_books.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly AuthorService _authorService;

        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            var authors = _authorService.GetAuthors();
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] AuthorViewModel author)
        {
            _authorService.AddAuthor(author);
            return Created(nameof(author), author);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateAuthor(int id, [FromBody] AuthorViewModel newAuthor)
        {
            var author = _authorService.UpdateAuthor(id, newAuthor);
            return Ok(author);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteAuthor(int id)
        {
            try
            {
                _authorService.DeleteAuthorById(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}
