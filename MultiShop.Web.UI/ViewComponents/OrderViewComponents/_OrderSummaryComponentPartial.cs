using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.BasketServices;

namespace MultiShop.Web.UI.ViewComponents.OrderViewComponents
{
    public class _OrderSummaryComponentPartial : ViewComponent
    {
        private readonly IBasketService _basketService;

        public _OrderSummaryComponentPartial(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var basketItems = (await _basketService.GetBasket()).BasketItems;
            return View(basketItems);
        }
    }
}
