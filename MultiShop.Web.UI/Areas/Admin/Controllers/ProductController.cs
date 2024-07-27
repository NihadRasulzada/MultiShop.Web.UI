using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.CategoryDtos;
using MultiShop.Web.Dto.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        //
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Home Page";
            ViewBag.v2 = "Categories";
            ViewBag.v3 = "Category Lists";
            ViewBag.v0 = "Category works";

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://157.230.105.226:7010/api/Product");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ResultProductDto>>(data);
                return View(products);
            }

            return View();
        }
    }
}
