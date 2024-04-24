using AuthServer.Interfaces;
using AuthServer.Models.Content.Services;
using Microsoft.AspNetCore.Mvc;

namespace FW_ASP.Net.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServices _services;

        public ServicesController(IServices services)
        {
            _services = services;
        }

        public IActionResult Index()
        {
            return View(_services.GetServices());
        }

        [Route("Services/Details")]
        public IActionResult Details(string id)
        {
            return View(_services.GetService(id));
        }
    }
}
