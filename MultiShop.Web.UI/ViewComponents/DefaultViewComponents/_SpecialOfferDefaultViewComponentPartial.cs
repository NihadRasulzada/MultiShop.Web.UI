using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CatalogServices.SpecialOfferServices;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _SpecialOfferDefaultViewComponentPartial : ViewComponent
    {
        private readonly ISpecialOfferService _specialOfferService;
        public _SpecialOfferDefaultViewComponentPartial(ISpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _specialOfferService.GetAllSpecialOfferAsync();
            return View(values);
        }
    }
}
