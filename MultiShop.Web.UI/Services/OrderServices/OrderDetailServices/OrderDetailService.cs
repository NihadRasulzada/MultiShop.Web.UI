using MultiShop.Web.Dto.OrderDtos.OrderDetailDtos;
using MultiShop.Web.Dto.OrderDtos.OrderOrderingDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.Services.OrderServices.OrderDetailServices
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly HttpClient _httpClient;
        private const string BASE_ENDPOINT = "OrderDetail";
        public OrderDetailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task CreateDetail(CreateOrderDetailDto createOrderDetailDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(BASE_ENDPOINT, createOrderDetailDto);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException("Failed to create about item", ex);
            }
        }

        public async Task DeleteDetail(int id)
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

        public async Task<List<ResultOrderDetailDto>> GetDetail()
        {
            try
            {
                var response = await _httpClient.GetAsync(BASE_ENDPOINT);
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ResultOrderDetailDto>>(jsonData)
                    ?? new List<ResultOrderDetailDto>();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException("Failed to retrieve about items", ex);
            }
        }

        public async Task<GetByIdOrderDetailDto> GetDetailById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BASE_ENDPOINT}/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<GetByIdOrderDetailDto>()
                    ?? throw new ApplicationException($"Failed to deserialize about item with ID: {id}");
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException($"Failed to retrieve about item with ID: {id}", ex);
            }
        }

        public async Task UpdateDetail(UpdateOrderDetailDto updateOrderDetailDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(BASE_ENDPOINT, updateOrderDetailDto);
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
