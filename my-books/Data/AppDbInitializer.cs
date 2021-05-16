using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using my_books.Context;
using my_books.Models;

namespace my_books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (context == null || context.Books.Any()) return;
                
                context.Books.AddRange(new Book()
                    {
                        Title = "The Little Prince",
                        Description = "A amazing book about loneliness, friendship, love and loss",
                        CoverUrl = "Https:Cover",
                        DateAdded = DateTime.Now,
                        DateRead = DateTime.Now.AddDays(-10),
                        Genre = "Children Literature, Drama",
                        IsRead = true,
                        Rate = 5,
                    },
                    new Book()
                    {
                        Title = "The Prince",
                        Description = "A book about ruling",
                        CoverUrl = "Https:Cover_prince",
                        DateAdded = DateTime.Now,
                        Genre = "History, Real-Life, Self Development",
                    });
                    
                context.SaveChanges();
            }
        }
    }
}