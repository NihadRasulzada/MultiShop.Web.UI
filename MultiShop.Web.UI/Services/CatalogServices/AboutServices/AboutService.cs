using MultiShop.Web.Dto.CatalogDtos.AboutDtos;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace MultiShop.Web.UI.Services.CatalogServices.AboutServices
{
    public class AboutService : IAboutService
    {
        private readonly HttpClient _httpClient;
        private const string BASE_ENDPOINT = "about";

        public AboutService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<ResultAboutDto>> GetAllAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(BASE_ENDPOINT);
                response.EnsureSuccessStatusCode();
                var jsonData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<ResultAboutDto>>(jsonData) 
                    ?? new List<ResultAboutDto>();
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException("Failed to retrieve about items", ex);
            }
        }

        public async Task<bool> CreateAsync(CreateAboutDto createAboutDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(BASE_ENDPOINT, createAboutDto);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException("Failed to create about item", ex);
            }
        }

        public async Task<bool> UpdateAsync(UpdateAboutDto updateAboutDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(BASE_ENDPOINT, updateAboutDto);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException("Failed to update about item", ex);
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{BASE_ENDPOINT}?id={id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException($"Failed to delete about item with ID: {id}", ex);
            }
        }

        public async Task<UpdateAboutDto> GetByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{BASE_ENDPOINT}/{id}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<UpdateAboutDto>() 
                    ?? throw new ApplicationException($"Failed to deserialize about item with ID: {id}");
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                throw new ApplicationException($"Failed to retrieve about item with ID: {id}", ex);
            }
        }
    }
}
