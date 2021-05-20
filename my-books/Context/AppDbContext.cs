using Microsoft.EntityFrameworkCore;
using my_books.Entities;
using my_books.Models.InputModel;

namespace my_books.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        

    public DbSet<Book> Books { get; set; }
    }
}