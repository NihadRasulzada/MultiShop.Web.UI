using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.BasketServices;
using MultiShop.Web.UI.Services.DiscountServices;

namespace MultiShop.Web.UI.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IBasketService _basketService;
        public DiscountController(IDiscountService discountService, IBasketService basketService)
        {
            _discountService = discountService;
            _basketService = basketService;
        }

        [HttpGet]
        public PartialViewResult ConfirmDiscountCoupon()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmDiscountCoupon(string code)
        {
            await _basketService.AddDiscount(code);

            var basketValues = await _basketService.GetBasket();

            //var totalPriceWithTax = basketValues.TotalPrice + basketValues.TotalPrice / 100 * 10;

            //var totalNewPriceWithDiscount = totalPriceWithTax - (totalPriceWithTax / 100 * basketValues.DiscountRate);

            //ViewBag.totalNewPriceWithDiscount = totalNewPriceWithDiscount;

            return RedirectToAction("Index", "ShoppingCart");
        }
    }
}
