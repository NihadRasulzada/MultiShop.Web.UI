using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.BasketDtos;
using MultiShop.Web.UI.Services.BasketServices;
using MultiShop.Web.UI.Services.CatalogServices.ProductServices;

namespace MultiShop.Web.UI.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IBasketService _basketService;
        public ShoppingCartController(IProductService productService, IBasketService basketService)
        {
            _productService = productService;
            _basketService = basketService;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _basketService.GetBasket();
            ViewBag.code = values.DiscountCode;
            ViewBag.discountRate = values.DiscountRate;
            ViewBag.totalNewPriceWithDiscount = values.TotalNewPriceWithDiscount;
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Sepetim";
            ViewBag.total = values.TotalPrice;
            ViewBag.totalPriceWithTax = values.TotalPriceWithTax;
            ViewBag.tax = values.Tax;
            return View();
        }

        //[HttpPost]
        public async Task<IActionResult> AddBasketItem(string id)
        {
            var values = await _productService.GetByIdProductAsync(id);
            var items = new BasketItemDto
            {
                ProductId = values.Id,
                ProductName = values.Name,
                Price = values.Price,
                Quantity = 1,
                ProductImageName = values.ImageName
            };
            await _basketService.AddBasketItem(items);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveBasketItem(string id)
        {
            await _basketService.RemoveBasketItem(id);
            return RedirectToAction("Index");
        }
    }
}
