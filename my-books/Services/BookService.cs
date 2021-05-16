using System;
using System.Collections.Generic;
using System.Linq;
using my_books.Context;
using my_books.Data.Models.Entities;
using my_books.Data.Models.Views;
using my_books.Models;
using my_books.Models.View;

namespace my_books.Services
{
    public class BookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public void AddBook(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                CoverUrl = book.CoverUrl,
                Description = book.Description,
                Genre = book.Genre,
                IsRead = book.IsRead,
                DateRead = book.IsRead? book.DateRead:null,
                Rate = book.IsRead? book.Rate:null,
                DateAdded = DateTime.Today,
                PublisherId = book.PublisherId,
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            foreach (var id in book.AuthorsIds)
            {
                var bookAuthor = new BookAuthor()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                _context.BookAuthors.Add(bookAuthor);
                _context.SaveChanges();
            }
        }

        public List<Book> GetBooks() => _context.Books.ToList();

        public Book GetBookById(int id) => _context.Books.FirstOrDefault(book => id == book.Id);

        public Book UpdateBookById(int id, BookVM book)
        {
            var oldBook = _context.Books.FirstOrDefault(b => b.Id == id);

            if (oldBook == null) return null;

            oldBook.Title = book.Title;
            oldBook.CoverUrl = book.CoverUrl;
            oldBook.Description = book.Description;
            oldBook.Genre = book.Genre;
            oldBook.IsRead = book.IsRead;
            oldBook.DateRead = book.IsRead? book.DateRead:null;
            oldBook.Rate = book.IsRead? book.Rate:null;

            _context.SaveChanges();

            return oldBook;

        }

        public void DeleteBookById(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null) return;
            
            _context.Books.Remove(book);
            _context.SaveChanges();

        }
    }
}
