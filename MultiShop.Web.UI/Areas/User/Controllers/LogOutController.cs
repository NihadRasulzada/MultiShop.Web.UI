using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.User.Controllers
{
    public class LogOutController : Controller
    {
        [Authorize]
        [Area(nameof(User))]
        public IActionResult Index()
        {
            return View();
        }
    }
}
