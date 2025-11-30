using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.ProductDtos;
using MultiShop.Web.Dto.OrderDtos.OrderDetailDtos;
using MultiShop.Web.Dto.OrderDtos.OrderOrderingDtos;
using MultiShop.Web.UI.Models;
using MultiShop.Web.UI.Services.CatalogServices.ProductServices;
using MultiShop.Web.UI.Services.ImageServices;
using MultiShop.Web.UI.Services.Interfaces;
using MultiShop.Web.UI.Services.OrderServices.OrderDetailServices;
using MultiShop.Web.UI.Services.OrderServices.OrderOderingServices;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class OrderController : Controller
    {
        private readonly IOrderOrderingService _orderOrderingService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly IUserService _userService;

        public OrderController(IOrderOrderingService orderOrderingService, IUserService userService, IOrderDetailService orderDetailService, IProductService productService, IImageService imageService)
        {
            _orderOrderingService = orderOrderingService;
            _userService = userService;
            _orderDetailService = orderDetailService;
            _productService = productService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            UserDetailViewModel user = await _userService.GetUserInfo();
            IEnumerable<ResultOrderOrderingByUserIdDto> orders = await _orderOrderingService.GetOrderingByUserId(user.Id);
            ViewBag.Orders = orders;
            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            IEnumerable<ResultOrderDetailDto> details = (await _orderDetailService.GetDetail()).Where(x => x.OrderingId == id);

            List<AdminOrderDetail> adminOrderDetails = new List<AdminOrderDetail>();
            foreach (var detail in details)
            {
                var product = await _productService.GetByIdProductAsync(detail.ProductId);
                var productImageLink = await _imageService.GetImageLinkAsync(product.ImageName);
                adminOrderDetails.Add(new AdminOrderDetail
                {
                    Id = detail.Id,
                    ProductId = detail.ProductId,
                    ProductName = product.Name,
                    ProductPrice = product.Price,
                    ProductImageLink = productImageLink,
                    ProductAmount = detail.ProductAmount,
                    OrderingId = detail.OrderingId
                });
            }
            

            return View(adminOrderDetails);
        }
    }
}
