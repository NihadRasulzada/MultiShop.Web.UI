using MultiShop.Web.Dto.BasketDtos;
using MultiShop.Web.Dto.DiscountDtos;
using MultiShop.Web.UI.Services.DiscountServices;
using MultiShop.Web.UI.Services.ImageServices;

namespace MultiShop.Web.UI.Services.BasketServices
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IImageService _imageService;
        private readonly IDiscountService _discountService;
        public BasketService(HttpClient httpClient, IImageService imageService, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _imageService = imageService;
            _discountService = discountService;
        }
        public async Task AddBasketItem(BasketItemDto basketItemDto)
        {
            BasketTotalDto values = await GetBasket();
            if (values != null)
            {
                if (!values.BasketItems.Any(x => x.ProductId == basketItemDto.ProductId))
                {
                    values.BasketItems.Add(basketItemDto);
                }
                else
                {
                    var updatedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == basketItemDto.ProductId);
                    updatedItem.Quantity += basketItemDto.Quantity;
                }
            }
            await SaveBasket(values);
        }

        public async Task AddDiscount(string discountCode)
        {
            var basketValues = await GetBasket();

            if (discountCode == null)
            {
                basketValues.DiscountCode = "";
                basketValues.DiscountRate = 0;
            }
            else
            {
                GetDiscountCodeDetailByCode values = await _discountService.GetDiscountCode(discountCode);
                basketValues.DiscountCode = values.Code;
                basketValues.DiscountRate = values.Rate;
            }
            await SaveBasket(basketValues);

        }

        public async Task DeleteBasket()
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"baskets");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException($"Failed to delete about item", ex);
            }
        }

        public async Task<BasketTotalDto> GetBasket()
        {
            var responseMessage = await _httpClient.GetAsync("baskets");
            var values = await responseMessage.Content.ReadFromJsonAsync<BasketTotalDto>();

            foreach (var item in values.BasketItems)
            {
                item.ProductImageLink = await _imageService.GetImageLinkAsync(item.ProductImageName);
            }
            return values;
        }

        public async Task<bool> RemoveBasketItem(string productId)
        {
            var values = await GetBasket();
            var deletedItem = values.BasketItems.FirstOrDefault(x => x.ProductId == productId);
            var result = values.BasketItems.Remove(deletedItem);
            await SaveBasket(values);
            return true;
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            if (basketTotalDto.DiscountCode == null)
            {
                basketTotalDto.DiscountCode = "";
                basketTotalDto.DiscountRate = 0;
            }
            var value = await _httpClient.PostAsJsonAsync<BasketTotalDto>("http://localhost:5000/services/basket/baskets", basketTotalDto);
        }
    }
}
