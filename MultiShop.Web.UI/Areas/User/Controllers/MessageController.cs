using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.User.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
