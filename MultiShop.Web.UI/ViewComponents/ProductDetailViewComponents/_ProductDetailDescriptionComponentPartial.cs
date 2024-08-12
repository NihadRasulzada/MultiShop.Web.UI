using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MultiShop.Web.Dto.CatalogDtos.ProductsDetailDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailDescriptionComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7010/api/ProductDetail/";

        public _ProductDetailDescriptionComponentPartial(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            var client = _clientFactory.CreateClient();

            var response = await client.GetAsync($"{ApiBaseUrl}GetByProductIdProductDetailAsync?id={productId}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var ProductDetail = JsonConvert.DeserializeObject<GetByIdProductDetailDto>(data);
                return View(ProductDetail);
            }
            return View();
        }
    }
}
