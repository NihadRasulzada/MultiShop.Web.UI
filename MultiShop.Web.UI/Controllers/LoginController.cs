using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.AuthDtos.LoginDtos;
using MultiShop.Web.UI.Services.Interfaces;

namespace MultiShop.Web.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IIdentityService _identityService;
        public LoginController(IHttpClientFactory httpClientFactory, IIdentityService identityService)
        {
            _httpClientFactory = httpClientFactory;
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SignInDto signInDto)
        {
            await _identityService.SignIn(signInDto);
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> SignIn(SignInDto signInDto)
        {
            signInDto.UserName = "Nihad";
            signInDto.Password = "1234567890Aa.";

            await _identityService.SignIn(signInDto);
            return RedirectToAction("Index", "User");
        }
    }
}
