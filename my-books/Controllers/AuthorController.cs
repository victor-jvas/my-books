using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Models.Views;
using my_books.Services;

namespace my_books.Controllers
{
    [Route("api/authors")]
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
            return Ok(author);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] AuthorViewModel author)
        {
            _authorService.AddAuthor(author);
            return Ok();
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
            _authorService.DeleteAuthorById(id);
            return Ok();
        }

    }
}
