using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    public class CategoryController : Controller    
    {
        [Area(nameof(Admin))]
        public IActionResult Index()
        {
            ViewBag.v1 = "Home Page";
            ViewBag.v2 = "Categories";
            ViewBag.v3 = "Category Lists";
            ViewBag.v0 = "Category works";
            return View();
        }
    }
}
