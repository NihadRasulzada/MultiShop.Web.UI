using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.FeatureSliderDto;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _CarouselDefaultViewComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public _CarouselDefaultViewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("http://157.230.105.226:7010/api/FeatureSlider/");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(data);
                return View(values);
            }
            return View();
        }
    }
}
