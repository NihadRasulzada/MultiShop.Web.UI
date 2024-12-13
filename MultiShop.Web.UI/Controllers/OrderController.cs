using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.OrderDtos.OrderAddressDtos;
using MultiShop.Web.UI.Services.Interfaces;
using MultiShop.Web.UI.Services.OrderServices.OrderAddressServices;

namespace MultiShop.Web.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderAddressService _orderAddressService;
        private readonly IUserService _userService;
        public OrderController(IOrderAddressService orderAddressService, IUserService userService)
        {
            _orderAddressService = orderAddressService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.directory1 = "MultiShop";
            ViewBag.directory2 = "Siparişler";
            ViewBag.directory3 = "Sipariş İşlemleri";


            GetByIdOrderAddressDto? address = await _orderAddressService.GetByUserIdAddressAsync((await _userService.GetUserInfo()).Id);

            if (address != null)
            {
                CreateOrderAddressDto value = new CreateOrderAddressDto()
                {
                    City = address.City,
                    Country = address.Country,
                    Description = address.Description,
                    Detail1 = address.Detail1,
                    Detail2 = address.Detail2,
                    District = address.District,
                    Email = address.Email,
                    Name = address.Name,
                    Phone = address.Phone,
                    Surname = address.Surname,
                    UserId = address.UserId,
                    ZipCode = address.ZipCode
                };
                return View(value);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddressSave(CreateOrderAddressDto createOrderAddressDto)
        {
            // Fetch user information and populate DTO
            var userInfo = await _userService.GetUserInfo();
            createOrderAddressDto.UserId = userInfo.Id;
            createOrderAddressDto.Description = "aa";

            // Check if an address already exists for the user
            var existingAddress = await _orderAddressService.GetByUserIdAddressAsync(createOrderAddressDto.UserId);
            if (existingAddress?.UserId != null)
            {
                // Map CreateOrderAddressDto to GetByIdOrderAddressDto
                var updateDto = MapToUpdateDto(createOrderAddressDto);
                updateDto.Id = existingAddress.Id;
                await _orderAddressService.UpdateOrderAddressAsync(updateDto);
                return RedirectToAction("Index","Order");
            }

            // Create a new address if none exists
            await _orderAddressService.CreateOrderAddressAsync(createOrderAddressDto);
            return RedirectToAction("Index","Order");
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateOrderAddressDto createOrderAddressDto)
        {
            return RedirectToAction("Index", "Payment");
        }

        // Helper method to map CreateOrderAddressDto to GetByIdOrderAddressDto
        private GetByIdOrderAddressDto MapToUpdateDto(CreateOrderAddressDto dto)
        {
            return new GetByIdOrderAddressDto
            {
                City = dto.City,
                Country = dto.Country,
                Description = dto.Description,
                Detail1 = dto.Detail1,
                Detail2 = dto.Detail2,
                District = dto.District,
                Email = dto.Email,
                Name = dto.Name,
                Phone = dto.Phone,
                Surname = dto.Surname,
                UserId = dto.UserId,
                ZipCode = dto.ZipCode
            };
        }

    }
}
