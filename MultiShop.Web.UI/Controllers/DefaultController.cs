using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services;

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
            var id = _loginService.GetUserId;
            var user = User.Claims;
            int x;
            return View();
        }
    }
}
