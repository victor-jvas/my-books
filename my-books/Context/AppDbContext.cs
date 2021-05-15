using Microsoft.EntityFrameworkCore;
using my_books.Models;

namespace my_books.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
    }
}