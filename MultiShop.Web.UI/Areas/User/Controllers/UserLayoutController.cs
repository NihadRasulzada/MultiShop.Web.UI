using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.User.Controllers
{
    [Area(nameof(User))]
    public class UserLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
