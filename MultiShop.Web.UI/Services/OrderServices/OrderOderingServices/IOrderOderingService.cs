using MultiShop.Web.Dto.OrderDtos.OrderOrderingDtos;

namespace MultiShop.Web.UI.Services.OrderServices.OrderOderingServices
{
    public interface IOrderOderingService
    {
        Task<List<ResultOrderingByUserIdDto>> GetOrderingByUserId(string id);
    }
}
