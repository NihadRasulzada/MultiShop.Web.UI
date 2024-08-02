using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.SpecialOfferDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _SpecialOfferDefaultViewComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;

        public _SpecialOfferDefaultViewComponentPartial(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://157.230.105.226:7010/api/SpecialOffer/");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var SpecialOffers = JsonConvert.DeserializeObject<List<ResultSpecialOfferDto>>(data);
                return View(SpecialOffers);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while fetching SpecialOffers.");
            }
            return View();
        }
    }
}
