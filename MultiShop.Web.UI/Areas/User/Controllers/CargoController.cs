using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.User.Controllers
{
    [Authorize]
    [Area(nameof(User))]
    public class CargoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
