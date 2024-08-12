using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.OfferDiscountDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _OfferDefaultViewComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _OfferDefaultViewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://157.230.105.226:7010/api/OfferDiscount/");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(jsonData);
                return View(values);
            }
            return View();
        }
    }
}
