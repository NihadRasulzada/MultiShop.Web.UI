using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.OrderDtos.OrderAddressDtos;
using MultiShop.Web.Dto.OrderDtos.OrderDetailDtos;
using MultiShop.Web.Dto.OrderDtos.OrderOrderingDtos;
using MultiShop.Web.UI.Services.BasketServices;
using MultiShop.Web.UI.Services.Interfaces;
using MultiShop.Web.UI.Services.OrderServices.OrderAddressServices;
using MultiShop.Web.UI.Services.OrderServices.OrderDetailServices;
using MultiShop.Web.UI.Services.OrderServices.OrderOderingServices;

namespace MultiShop.Web.UI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IOrderOrderingService _orderOrderingService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderAddressService _orderAddressService;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        public PaymentController(IOrderOrderingService orderOrderingService, IOrderDetailService orderDetailService, IOrderAddressService orderAddressService, IUserService userService, IBasketService basketService)
        {
            _orderOrderingService = orderOrderingService;
            _orderDetailService = orderDetailService;
            _orderAddressService = orderAddressService;
            _userService = userService;
            _basketService = basketService;
        }

        public IActionResult Index()
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory2 = "Ödeme Ekranı";
            ViewBag.directory3 = "Kartla Ödeme";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Pay()
        {
            var user = await _userService.GetUserInfo();
            var basket = await _basketService.GetBasket();
            var address = await _orderAddressService.GetByUserIdAddressAsync(user.Id);

            await _orderOrderingService.CreateOrdering(new CreateOrderOrderingDto()
            {
                UserId = user.Id,
                TotalPrice = basket.TotalNewPriceWithDiscount + 12,
                AddressId = address.Id
            });

            ResultOrderOrderingByUserIdDto order = (await _orderOrderingService.GetOrderingByUserId(user.Id)).LastOrDefault();

            foreach (var item in basket.BasketItems)
            {
                await _orderDetailService.CreateDetail(new CreateOrderDetailDto()
                {
                    OrderingId = order.Id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductPrice = item.Price,
                    ProductAmount = item.Quantity,
                    ProductTotalPrice = item.Price * item.Quantity
                });
            }

           await _basketService.DeleteBasket();



            return RedirectToAction("Index", "Home");
        }
    }
}
