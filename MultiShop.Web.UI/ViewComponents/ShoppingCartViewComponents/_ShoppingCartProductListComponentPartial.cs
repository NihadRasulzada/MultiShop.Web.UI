using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.BasketServices;

namespace MultiShop.Web.UI.ViewComponents.ShoppingCartViewComponents
{
    public class _ShoppingCartProductListComponentPartial : ViewComponent
    {
        private readonly IBasketService _basketService;
        public _ShoppingCartProductListComponentPartial(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _basketService.GetBasket();
            return View(values.BasketItems);
        }
    }
}
