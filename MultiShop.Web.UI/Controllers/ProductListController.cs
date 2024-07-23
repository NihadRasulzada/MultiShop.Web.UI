using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Controllers
{
    public class ProductListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductDetail()
        {
            return View();
        }
    }
}
