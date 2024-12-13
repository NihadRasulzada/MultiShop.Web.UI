using Microsoft.CodeAnalysis.CSharp.Syntax;
using MultiShop.Web.Dto.CatalogDtos.AboutDtos;
using MultiShop.Web.Dto.OrderDtos.OrderOrderingDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.Services.OrderServices.OrderOderingServices
{
    public class OrderOrderingService : IOrderOrderingService
    {
        private readonly HttpClient _httpClient;
        private const string BASE_ENDPOINT = "orderings";
        public OrderOrderingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOrdering(CreateOrderOrderingDto createOrderOrderingDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(BASE_ENDPOINT, createOrderOrderingDto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException("Failed to create about item", ex);
            }
        }

        public async Task DeleteOrdering(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BASE_ENDPOINT}?id={id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException($"Failed to delete about item with ID: {id}", ex);
            }
        }

        public async Task<List<ResultOrderOrderingDto>> GetOrdering()
        {
            try
            {
                var response = await _httpClient.GetAsync(BASE_ENDPOINT);
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ResultOrderOrderingDto>>(jsonData)
                    ?? new List<ResultOrderOrderingDto>();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException("Failed to retrieve about items", ex);
            }
        }

        public async Task<GetByIdOrderOrderingDto> GetOrderingById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BASE_ENDPOINT}/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GetByIdOrderOrderingDto>()
                    ?? throw new ApplicationException($"Failed to deserialize about item with ID: {id}");
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException($"Failed to retrieve about item with ID: {id}", ex);
            }
        }

        public async Task<List<ResultOrderOrderingByUserIdDto>> GetOrderingByUserId(string id)
        {
            var responseMessage = await _httpClient.GetAsync($"orderings/GetOrderingByUserId/{id}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultOrderOrderingByUserIdDto>>(jsonData);
            return values;
        }

        public async Task UpdateOrdering(UpdateOrderOrderingDto updateOrderOrderingDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(BASE_ENDPOINT, updateOrderOrderingDto);
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
