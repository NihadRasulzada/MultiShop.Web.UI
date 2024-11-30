using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class AdminLayoutController : Controller
    {
        public IActionResult _AdminLayout()
        {
            return View();
        }
    }
}
