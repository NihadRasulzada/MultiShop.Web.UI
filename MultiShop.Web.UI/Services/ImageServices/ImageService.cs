using MultiShop.Web.Dto.CatalogDtos.BrandsDto;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Linq;

namespace MultiShop.Web.UI.Services.ImageServices
{
    public class ImageService : IImageService
    {
        private readonly HttpClient _httpClient;
        public ImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> CreateImageAsync(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                throw new ArgumentException("Invalid photo file.", nameof(photo));

            using var content = new MultipartFormDataContent();
            using var stream = photo.OpenReadStream();
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(photo.ContentType);
            content.Add(fileContent, "file", photo.FileName);

            HttpResponseMessage response = await _httpClient.PostAsync("Image", content);

            response.EnsureSuccessStatusCode();

            string values = await response.Content.ReadAsStringAsync();

            return values;
        }


        public async Task DeleteImageAsync(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                throw new ArgumentException("Image name cannot be null or empty.", nameof(imageName));

            var response = await _httpClient.DeleteAsync($"Image/DeleteImage?fileName={imageName}");

            response.EnsureSuccessStatusCode();
        }


        public async Task<string> GetImageLinkAsync(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
                throw new ArgumentException("The provided image name is null, empty, or contains only whitespace.", nameof(imageName));

            string requestUri = $"http://localhost:7077/api/Image?fileName={Uri.EscapeDataString(imageName)}";

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.GetAsync(requestUri);
            }
            catch (Exception ex)
            {
                throw new HttpRequestException("An error occurred while sending the HTTP request.", ex);
            }

            if (!response.IsSuccessStatusCode)
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"The server responded with an error. StatusCode: {response.StatusCode}, Content: {errorContent}");
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            return responseContent;
        }
    }
}
