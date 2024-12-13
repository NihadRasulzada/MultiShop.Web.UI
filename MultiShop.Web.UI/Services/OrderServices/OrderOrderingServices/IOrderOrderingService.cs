using MultiShop.Web.Dto.OrderDtos.OrderOrderingDtos;

namespace MultiShop.Web.UI.Services.OrderServices.OrderOderingServices
{
    public interface IOrderOrderingService
    {
        Task<List<ResultOrderOrderingByUserIdDto>> GetOrderingByUserId(string id); 
        Task CreateOrdering(CreateOrderOrderingDto createOrderOrderingDto); 
        Task UpdateOrdering(UpdateOrderOrderingDto updateOrderOrderingDto); 
        Task DeleteOrdering(int id); 
        Task<GetByIdOrderOrderingDto> GetOrderingById(int id); 
        Task<List<ResultOrderOrderingDto>> GetOrdering(); 
    }
}
