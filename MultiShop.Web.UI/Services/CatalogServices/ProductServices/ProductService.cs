using MultiShop.Web.Dto.CatalogDtos.BrandsDto;
using MultiShop.Web.Dto.CatalogDtos.ProductDtos;
using MultiShop.Web.UI.Services.ImageServices;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.Services.CatalogServices.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IImageService _imageService;
        public ProductService(HttpClient httpClient, IImageService imageService)
        {
            _httpClient = httpClient;
            _imageService = imageService;
        }
        public async Task CreateProductAsync(CreateProductDto createProductDto)
        {
            string imageName = await _imageService.CreateImageAsync(createProductDto.Photo);
            createProductDto.ImageName = imageName;
            await _httpClient.PostAsJsonAsync<CreateProductDto>("product", createProductDto);
        }
        public async Task DeleteProductAsync(string id)
        {
            await _httpClient.DeleteAsync("product?id=" + id);
        }
        public async Task<UpdateProductDto> GetByIdProductAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("product/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateProductDto>();
            values.ImageLink = await _imageService.GetImageLinkAsync(values.ImageName);
            return values;
        }
        public async Task<List<ResultProductDto>> GetAllProductAsync()
        {
            var responseMessage = await _httpClient.GetAsync("product");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData);

            foreach (var item in values)
            {
                item.ImageLink = await _imageService.GetImageLinkAsync(item.ImageName);
            }

            return values;
        }
        public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto.Photo != null)
            {
                string imageName = await _imageService.CreateImageAsync(updateProductDto.Photo);
                updateProductDto.ImageName = imageName;
            }
            await _httpClient.PutAsJsonAsync<UpdateProductDto>("product", updateProductDto);
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            var responseMessage = await _httpClient.GetAsync("product");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);

            foreach (var item in values)
            {
                item.ImageLink = await _imageService.GetImageLinkAsync(item.ImageName);
            }

            return values;
        }

        public async Task<List<ResultProductWithCategoryDto>> GetProductsWithCategoryByCatetegoryIdAsync(string CategoryId)
        {
            var responseMessage = await _httpClient.GetAsync($"product/ProductListWithCategoryByCategoryId/{CategoryId}");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData);

            foreach (var item in values)
            {
                item.ImageLink = await _imageService.GetImageLinkAsync(item.ImageName);
            }

            return values;
        }
    }
}
