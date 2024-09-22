using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.Interfaces;

namespace MultiShop.Web.UI.Controllers
{
    public class DefaultController : Controller
    {
        private readonly ILoginService _loginService;

        public DefaultController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
