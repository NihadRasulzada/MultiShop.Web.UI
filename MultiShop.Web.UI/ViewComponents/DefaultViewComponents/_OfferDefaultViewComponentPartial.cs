using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CatalogServices.OfferDiscountServices;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _OfferDefaultViewComponentPartial : ViewComponent
    {
        private readonly IOfferDiscountService _offerDiscountService;
        public _OfferDefaultViewComponentPartial(IOfferDiscountService offerDiscountService)
        {
            _offerDiscountService = offerDiscountService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _offerDiscountService.GetAllOfferDiscountAsync();
            return View(values);
        }
    }
}
