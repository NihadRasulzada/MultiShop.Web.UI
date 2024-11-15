using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.BasketDtos;
using MultiShop.Web.Dto.CatalogDtos.ProductDtos;
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
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Sepetim";
            return View();
        }


        public async Task<IActionResult> AddBasketItem(string id)
        {
            UpdateProductDto values = await _productService.GetByIdProductAsync(id);
            var items = new BasketItemDto
            {
                ProductId = values.Id,
                ProductName = values.Name,
                Price = values.Price,
                Quantity = 1,
                ProductImageUrl = values.ImageUrl
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
