using AuthServer.Interfaces;
using AuthServer.Models.Content.Requests;
using FW_ASP.Net.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FW_ASP.Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRequest _requests;
        private readonly ITitle _title;
        private readonly IContacts _contacts;

        public HomeController(
            ILogger<HomeController> logger, 
            IRequest requests, 
            ITitle title,
            IContacts contacts)
        {
            _logger = logger;
            _requests = requests;
            _title = title;
            _contacts = contacts;
        }

        public IActionResult Index()
        {
            var t = _title.GetTitles().ElementAt(0);
            return View(new MainPageModel(_requests.GetFWRequest(), t));
        }
        
        public async Task<IActionResult> PostRequest(MainPageModel mainPageModel)
        {
            var r = await _requests.CreateRequest(mainPageModel.WRequest);
            if (r.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("ResultInfo");
            }
            else
            {
                return RedirectToAction("RequestFailed");
            }
        }

        public IActionResult ResultInfo()
        {
            return View();
        }

        public IActionResult RequestFailed()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            var t = _title.GetTitles().ElementAt(0);
            var c = _contacts.GetContacts().ElementAt(0);
            return View(new ContactsPageModel(t, c));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
