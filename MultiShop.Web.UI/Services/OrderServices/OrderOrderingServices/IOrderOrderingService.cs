using MultiShop.Web.Dto.OrderDtos.OrderOrderingDtos;

namespace MultiShop.Web.UI.Services.OrderServices.OrderOderingServices
{
    public interface IOrderOrderingService
    {
        Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string id);
    }
}
