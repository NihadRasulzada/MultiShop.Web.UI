using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.Interfaces;

namespace MultiShop.Web.UI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory2 = "Home Page";
            ViewBag.directory2 = "Product List";

            return View();
        }
    }
}
