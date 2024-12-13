using MultiShop.Web.Dto.CatalogDtos.AboutDtos;
using MultiShop.Web.Dto.OrderDtos.OrderAddressDtos;

namespace MultiShop.Web.UI.Services.OrderServices.OrderAddressServices
{
    public class OrderAddressService : IOrderAddressService
    {
        private readonly HttpClient _httpClient;
        private const string BASE_ENDPOINT = "Addresses";
        public OrderAddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto)
        {
            await _httpClient.PostAsJsonAsync<CreateOrderAddressDto>(BASE_ENDPOINT, createOrderAddressDto);
        }

        public async Task<GetByIdOrderAddressDto?> GetByUserIdAddressAsync(string id)
        {
            try
            {
                var endpoint = $"{BASE_ENDPOINT}/User/{id}";
                var response = await _httpClient.GetAsync(endpoint);

                response.EnsureSuccessStatusCode();

                if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return null;
                }

                var result = await response.Content.ReadFromJsonAsync<GetByIdOrderAddressDto>();

                return result;
            }
            catch (Exception ex)
            {
                // Replace with an actual logging framework
                Console.Error.WriteLine($"An error occurred while processing ID {id}: {ex.Message}");
                throw new ApplicationException($"Error while retrieving user address details for ID: {id}", ex);
            }
        }

        public async Task UpdateOrderAddressAsync(GetByIdOrderAddressDto updateAddressDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(BASE_ENDPOINT, updateAddressDto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException("Failed to update about item", ex);
            }
        }
    }
}
