using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_books.Context;
using my_books.Data;
using my_books.Entities;
using my_books.Exceptions;
using my_books.Models;
using my_books.Models.InputModel;
using my_books.Models.ViewModel;

namespace my_books.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BookViewModel> AddBook(BookInputModel bookInputModel)
        {
            var checkBook = _context.Books.FirstOrDefault(b => b.Title == bookInputModel.Title && b.Publisher == bookInputModel.Publisher);

            if (checkBook == null) throw new BookAlreadyExistException();

            var book = new Book()
            {
                Title = bookInputModel.Title,
                Description = bookInputModel.Description,
                Genre = bookInputModel.Genre,
                Author = bookInputModel.Author,
                Publisher = bookInputModel.Publisher,
                IsRead = bookInputModel.IsRead,
                DateRead = bookInputModel.IsRead? bookInputModel.DateRead:null,
                Rate = bookInputModel.IsRead? bookInputModel.Rate:null,
                DateAdded = DateTime.Today,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            var bookViewModel = new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Genre = book.Genre,
                Author = book.Author,
                Publisher = book.Publisher,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate : null,
                DateAdded = book.DateAdded,
            };

            return bookViewModel;
        }

        public async Task<List<BookViewModel>> GetBooks(int page, int perPage)
        {
            var books = _context.Books.ToList();

            var pageBook = PaginatedList<Book>.Create(books.AsQueryable(), page, perPage);
            
            return pageBook.Select(book => new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Genre = book.Genre,
                Author = book.Author,
                IsRead = book.IsRead,
                DateAdded = book.DateAdded,
                DateRead = book.DateRead,
                Publisher = book.Publisher,
                Rate = book.Rate
            })
                .ToList();
        }

        public async Task<BookViewModel> GetBookById(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null) return null;

            var bookViewModel = new BookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Description = book.Description,
                Genre = book.Genre,
                Author = book.Author,
                IsRead = book.IsRead,
                DateAdded = book.DateAdded,
                DateRead = book.DateRead,
                Publisher = book.Publisher,
                Rate = book.Rate
            };
            
            return bookViewModel;
        }

        public async Task UpdateBookById(int id, BookInputModel book)
        {
            var oldBook = _context.Books.FirstOrDefault(b => b.Id == id);

            if (oldBook == null) throw new BookNotRegisteredException();

            oldBook.Title = book.Title;
            oldBook.Description = book.Description;
            oldBook.Genre = book.Genre;
            oldBook.Author = book.Author;
            oldBook.Publisher = book.Publisher;
            oldBook.IsRead = book.IsRead;
            oldBook.DateRead = book.IsRead? book.DateRead:null;
            oldBook.Rate = book.IsRead? book.Rate:null;

            await _context.SaveChangesAsync();

        }

        public async Task DeleteBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) throw new BookNotRegisteredException();
            
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
