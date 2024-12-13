using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
