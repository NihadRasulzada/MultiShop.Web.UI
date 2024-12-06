using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.Interfaces;
using MultiShop.Web.UI.Services.OrderServices.OrderOderingServices;

namespace MultiShop.Web.UI.Areas.User.Controllers
{
    [Area(nameof(User))]
    public class MyOrderController : Controller
    {
        private readonly IOrderOrderingService _orderOderingService;
        private readonly IUserService _userService;
        public MyOrderController(IOrderOrderingService orderOderingService, IUserService userService)
        {
            _orderOderingService = orderOderingService;
            _userService = userService;
        }
        public async Task<IActionResult> MyOrderList()
        {
            var user = await _userService.GetUserInfo();
            var values = await _orderOderingService.GetOrderingByUserId(user.Id);
            return View(values);
        }
    }
}
