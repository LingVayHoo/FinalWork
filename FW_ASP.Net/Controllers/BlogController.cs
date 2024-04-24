using AuthServer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FW_ASP.Net.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlog _blog;

        public BlogController(IBlog blog)
        {
            _blog = blog;
        }

        public IActionResult Index()
        {
            return View(_blog.GetBlogItem());
        }

        [Route("Blog/Details")]
        public IActionResult Details(string id)
        {
            return View(_blog.GetBlogItem(id));
        }
    }
}
