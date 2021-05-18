using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using my_books.Context;
using my_books.Data.Models.Views;
using my_books.Exceptions;
using my_books.Models;

namespace my_books.Services
{
    public class PublisherService
    {
        private AppDbContext _context { get; set; }

        public PublisherService(AppDbContext context)
        {
            _context = context;
        }

        public void AddPublisher([FromBody]PublisherViewModel publisher)
        {

            if (StringStartsWithNumber(publisher.Name))
            {
                throw new PublisherNameException("Name starts with number", publisher.Name);
            }
            
            
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

        }

        public List<Publisher> GetPublishers() => _context.Publishers.ToList();

        public PublisherAllInfoViewModel GetPublisherById(int id)
        {
            
            var publisherData = _context.Publishers.Where(n => n.Id == id)
                .Select(n => new PublisherAllInfoViewModel()
            {
                Name = n.Name,
                BookAuthors = n.Books.Select(n => new BookAuthorViewModel()
                {
                    BookName = n.Title,
                    BookAuthors = n.BookAuthors.Select(n => n.Author.Fullname).ToList()
                }).ToList()
            }).FirstOrDefault();

            return publisherData;
        }

        public Publisher UpdatePublisher(int id, [FromBody] PublisherViewModel newPublisher)
        {
            var publisher = _context.Publishers.Find(id);

            if (publisher == null) return null;

            publisher.Name = newPublisher.Name;

            _context.SaveChanges();

            return publisher;
        }

        public void DeletePublisherById(int id)
        {
            var publisher = _context.Publishers.Find(id);

            if(publisher == null) return;

            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
        }

        private bool StringStartsWithNumber(string publisherName)
        {
            return Regex.IsMatch(publisherName, @"^\d");
        }
    }

}
