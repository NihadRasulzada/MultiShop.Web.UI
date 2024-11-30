using MultiShop.Web.Dto.BasketDtos;

namespace MultiShop.Web.UI.Services.BasketServices
{
    public interface IBasketService 
    {
        Task<BasketTotalDto> GetBasket();
        Task SaveBasket(BasketTotalDto basketTotalDto);
        Task DeleteBasket(string userId);
        Task AddBasketItem(BasketItemDto basketItemDto);
        Task<bool> RemoveBasketItem(string productId);
    }
}
