using Microsoft.AspNetCore.Mvc;

namespace Odev_1_MelikeYilmaz.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
