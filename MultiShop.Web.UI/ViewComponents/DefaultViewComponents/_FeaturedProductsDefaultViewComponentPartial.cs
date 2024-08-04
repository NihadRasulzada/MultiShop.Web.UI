using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.CategoryDtos;
using MultiShop.Web.Dto.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _FeaturedProductsDefaultViewComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _FeaturedProductsDefaultViewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://157.230.105.226:7010/api/Product/");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
