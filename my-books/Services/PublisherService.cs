using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using my_books.Context;
using my_books.Models;
using my_books.Models.View;

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
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

        }

        public List<Publisher> GetPublishers() => _context.Publishers.ToList();

        public Publisher GetPublisherById(int id) => _context.Publishers.Find(id);

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
    }

}
