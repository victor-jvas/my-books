using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using my_books.Context;
using my_books.Models.InputModel;
using my_books.Models.ViewModel;

namespace my_books.Services
{
    public class AuthorService
    {
        private AppDbContext Context { get; set; }

        public AuthorService(AppDbContext context)
        {
            Context = context;
        }

        public void AddAuthor([FromBody]AuthorViewModel newAuthor)
        {
            var author = new Author()
            {
                Fullname = newAuthor.Fullname
            };

            Context.Authors.Add(author);
            Context.SaveChanges();

        }

        public List<Author> GetAuthors() => Context.Authors.ToList();

        public AuthorWithBooksViewModel GetAuthorById(int id)
        {
            var authorWithBooks = Context.Authors.Where(n => n.Id == id).Select(author =>
                new AuthorWithBooksViewModel()
                {
                    Fullname = author.Fullname,
                    BookTitles = author.BookAuthors.Select(n => n.Book.Title).ToList()
                }).FirstOrDefault();
            
            return authorWithBooks;
        }

        public Author UpdateAuthor(int id, [FromBody] AuthorViewModel newAuthor)
        {
            var author = Context.Authors.Find(id);

            if (author == null) return null;

            author.Fullname = newAuthor.Fullname;

            Context.SaveChanges();

            return author;
        }

        public void DeleteAuthorById(int id)
        {
            var author = Context.Authors.Find(id);

            if(author == null) throw new Exception($"Author with id: {id} not found.");

            Context.Authors.Remove(author);
            Context.SaveChanges();
        }
    }

}
