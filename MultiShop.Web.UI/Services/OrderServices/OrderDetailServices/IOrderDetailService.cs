using MultiShop.Web.Dto.OrderDtos.OrderDetailDtos;

namespace MultiShop.Web.UI.Services.OrderServices.OrderDetailServices
{
    public interface IOrderDetailService
    {
        Task CreateDetail(CreateOrderDetailDto createOrderDetailDto);
        Task UpdateDetail(UpdateOrderDetailDto updateOrderDetailDto);
        Task DeleteDetail(int id);
        Task<GetByIdOrderDetailDto> GetDetailById(int id);
        Task<List<ResultOrderDetailDto>> GetDetail();
    }
}
