using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    public class AdminLayoutController : Controller
    {
        [Area(nameof(Admin))]
        public IActionResult _AdminLayout()
        {
            return View();
        }
    }
}
