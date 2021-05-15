using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}