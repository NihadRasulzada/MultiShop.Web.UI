using MultiShop.Web.Dto.OrderDtos.OrderAddressDtos;

namespace MultiShop.Web.UI.Services.OrderServices.OrderAddressServices
{
    public interface IOrderAddressService
    {
        // Task<List<ResultAboutDto>> GetAllAboutAsync();
        Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto);
        Task UpdateOrderAddressAsync(GetByIdOrderAddressDto updateAddressDto);
        //    Task DeleteAboutAsync(string id);
        Task<GetByIdOrderAddressDto?> GetByUserIdAddressAsync(string id);
    }
}
