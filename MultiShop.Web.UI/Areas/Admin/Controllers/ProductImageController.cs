using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.ProducImageDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]

    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7010/api/";
        //private const string ApiBaseUrl = "http://localhost:7070/api/";
        private const string ViewBagV1 = "Home Page";
        private const string ViewBagV2 = "ProductImages";
        private const string ViewBagV3 = "ProductImage Lists";
        private const string ViewBagV0 = "ProductImage works";

        public ProductImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private void SetViewBagValues()
        {
            ViewBag.v1 = ViewBagV1;
            ViewBag.v2 = ViewBagV2;
            ViewBag.v3 = ViewBagV3;
            ViewBag.v0 = ViewBagV0;
        }


        public async Task<IActionResult> Update(string productId)
        {
            SetViewBagValues();

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{ApiBaseUrl}ProductImage/GetByProductIdProductImagesAsync?id={productId}");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdProductImageDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(string productId, GetByIdProductImageDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(dto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{ApiBaseUrl}ProductImage/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductsWithCategory", "Product", new { area = nameof(Admin) });
            }

            return View();
        }
    }
}
