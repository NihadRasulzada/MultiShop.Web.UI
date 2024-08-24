using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.AuthDtos.RegisterDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.Web.UI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7050/api/User";

        public RegisterController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateRegisterDto createRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createRegisterDto);
            }

            if (createRegisterDto.Password == createRegisterDto.ConfirmPassword)
            {
                var client = _clientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createRegisterDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(ApiBaseUrl, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid data");
                }
            }
            else
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match");
            }

            return View(createRegisterDto);
        }
    }
}
