using Microsoft.AspNetCore.Mvc;
using my_books.Models.View;
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
        public IActionResult GetPublishers()
        {
            var publishers = _publisherService.GetPublishers();
            return Ok(publishers);
        }

        
        [HttpGet("{id:int}")]
        public IActionResult GetPublisherById(int id)
        {
            var publisher = _publisherService.GetPublisherById(id);
            return Ok(publisher);
        }

        [HttpPost]
        public IActionResult AddPublisher([FromBody]PublisherViewModel publisher)
        {
            _publisherService.AddPublisher(publisher);
            return Ok();
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