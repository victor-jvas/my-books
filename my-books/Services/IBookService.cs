using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using my_books.Models.InputModel;
using my_books.Models.ViewModel;

namespace my_books.Services
{
    public interface IBookService : IDisposable
    {
        Task<List<BookViewModel>> GetBooks(int page, int perPage);
        Task<BookViewModel> GetBookById(int bookId);
        Task<BookViewModel> AddBook(BookInputModel bookInputModel);
        Task UpdateBookById(int bookId, BookInputModel bookInputModel);
        Task DeleteBookById(int bookId);
    }
}