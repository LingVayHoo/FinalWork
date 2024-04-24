using Microsoft.AspNetCore.Mvc;

namespace FW_ASP.Net.Component
{
    public class LogoutViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

