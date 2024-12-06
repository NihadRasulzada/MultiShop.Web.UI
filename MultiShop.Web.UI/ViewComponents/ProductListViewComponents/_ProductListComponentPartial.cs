using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CatalogServices.ProductServices;

namespace MultiShop.Web.UI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProductService _productService;
        public _ProductListComponentPartial(IHttpClientFactory httpClientFactory, IProductService productService)
        {
            _httpClientFactory = httpClientFactory;
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var products = await _productService.GetProductsWithCategoryByCatetegoryIdAsync(id);
            return View(products);
        }
    }
}
//ProductsWithCategoryByCategoryId