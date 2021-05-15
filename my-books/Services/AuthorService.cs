using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using my_books.Context;
using my_books.Data.Models.Entities;
using my_books.Data.Models.Views;
using my_books.Models.View;

namespace my_books.Services
{
    public class AuthorService
    {
        private AppDbContext _context { get; set; }

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor([FromBody]AuthorViewModel newAuthor)
        {
            var author = new Author()
            {
                Fullname = newAuthor.Fullname
            };

            _context.Authors.Add(author);
            _context.SaveChanges();

        }

        public List<Author> GetAuthors() => _context.Authors.ToList();

        public Author GetAuthorById(int id) => _context.Authors.Find(id);

        public Author UpdateAuthor(int id, [FromBody] AuthorViewModel newAuthor)
        {
            var author = _context.Authors.Find(id);

            if (author == null) return null;

            author.Fullname = newAuthor.Fullname;

            _context.SaveChanges();

            return author;
        }

        public void DeleteAuthorById(int id)
        {
            var author = _context.Authors.Find(id);

            if(author == null) return;

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }

}
