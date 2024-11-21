using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.User.Controllers
{
    public class MyOrderController : Controller
    {
        public IActionResult MyOrderList()
        {
            return View();
        }
    }
}
