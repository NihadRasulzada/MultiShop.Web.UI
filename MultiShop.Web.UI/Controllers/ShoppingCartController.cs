using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
