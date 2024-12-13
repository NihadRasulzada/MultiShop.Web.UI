using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.User.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize]
        [Area(nameof(User))]
        public IActionResult Index()
        {
            return View();
        }
    }
}
