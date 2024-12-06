using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CatalogServices.ProductImageServices;

namespace MultiShop.Web.UI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailImageSliderComponentPartial : ViewComponent
    {
        private readonly IProductImageService _productImageService;

        public _ProductDetailImageSliderComponentPartial(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var productImages = await _productImageService.GetByProductIdProductImageAsync(id);
            return View(productImages);
        }
    }
}
