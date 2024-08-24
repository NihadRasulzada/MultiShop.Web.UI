using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.AuthDtos.LoginDtos;
using MultiShop.Web.UI.Models;
using MultiShop.Web.UI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace MultiShop.Web.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILoginService _loginService;

        public LoginController(IHttpClientFactory clientFactory, ILoginService loginService)
        {
            _clientFactory = clientFactory;
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateLoginDto createLoginDto)
        {
            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(createLoginDto), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://157.230.105.226:7050/api/User/Login", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                if (tokenModel != null)
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(tokenModel.AccessToken);
                    var claims = token.Claims.ToList();

                    if (tokenModel.AccessToken != null)
                    {
                        claims.Add(new Claim("multishoptoken", tokenModel.AccessToken));
                        var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                        var authProps = new AuthenticationProperties
                        {
                            ExpiresUtc = tokenModel.AccessTokenExpiration,
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
                        return RedirectToAction("Index", "Default");
                    }
                }
            }
            return View();
        }
    }
}
