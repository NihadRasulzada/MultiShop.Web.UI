using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CatalogServices.BrandServices;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _VendorDefaultViewComponentPartial : ViewComponent
    {
        private readonly IBrandService _brandService;
        public _VendorDefaultViewComponentPartial(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _brandService.GetAllBrandAsync();
            return View(values);
        }
    }
}
