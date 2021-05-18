using System;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Models.Views;
using my_books.Exceptions;
using my_books.Services;

namespace my_books.Controllers
{
    [Route("api/publishers")]
    [ApiController]
    public class PublisherController : Controller
    {
        private readonly PublisherService _publisherService;

        public PublisherController(PublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public IActionResult GetPublishers(string sortBy, string searchString, int pageNumber)
        {
            var publishers = _publisherService.GetPublishers(sortBy, searchString, pageNumber);
            return Ok(publishers);
        }

        
        [HttpGet("{id:int}")]
        public IActionResult GetPublisherById(int id)
        {
            var publisher = _publisherService.GetPublisherById(id);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }

        [HttpPost]
        public IActionResult AddPublisher([FromBody]PublisherViewModel publisher)
        {
            try
            {

                _publisherService.AddPublisher(publisher);
                return Created(nameof(publisher), publisher);
            }
            catch (PublisherNameException exception)
            {
                return BadRequest($"{exception.Message}, Publisher name: {exception.PublisherName}");
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;  
            }
        }

        [HttpPut]
        public IActionResult UpdatePublisher(int id, [FromBody] PublisherViewModel publisher)
        {
            var pub = _publisherService.UpdatePublisher(id, publisher);
            return Ok(pub);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePublisher(int id)
        {
            _publisherService.DeletePublisherById(id);
            return Ok();
        }

    }
}