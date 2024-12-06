using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CatalogServices.ProductServices;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _FeaturedProductsDefaultViewComponentPartial : ViewComponent
    {
        private readonly IProductService _productService;
        public _FeaturedProductsDefaultViewComponentPartial(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _productService.GetAllProductAsync();
            return View(values);
        }
    }
}
