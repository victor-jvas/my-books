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
using my_books.Data;
using my_books.Exceptions;
using my_books.Models;
using my_books.Models.InputModel;
using my_books.Models.ViewModel;

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

        public List<Publisher> GetPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var allPublishers = _context.Publishers.OrderBy(n=>n.Name).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allPublishers = allPublishers.Where(n => n.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }

            const int pageSize = 3;
            allPublishers = PaginatedList<Publisher>.Create(allPublishers.AsQueryable(), pageNumber ?? 1, pageSize);

            return allPublishers;
        }

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
