using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CatalogServices.ProductDetailServices;

namespace MultiShop.Web.UI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailInformationComponentPartial : ViewComponent
    {
        private readonly IProductDetailService _productDetailService;

        public _ProductDetailInformationComponentPartial(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            var values = await _productDetailService.GetByProductIdProductDetailAsync(productId);
            return View(values);
        }
    }
}
