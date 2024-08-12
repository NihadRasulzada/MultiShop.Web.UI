using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.ProductDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailFeatureComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailFeatureComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://157.230.105.226:7010/api/Product/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
