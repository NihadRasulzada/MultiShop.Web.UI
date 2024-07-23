using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
