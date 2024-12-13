using MultiShop.Web.Dto.CatalogDtos.BrandsDto;
using MultiShop.Web.UI.Services.ImageServices;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.Services.CatalogServices.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly HttpClient _httpClient;
        private readonly IImageService _imageService;
        public BrandService(HttpClient httpClient, IImageService imageService)
        {
            _httpClient = httpClient;
            _imageService = imageService;
        }
        public async Task CreateBrandAsync(CreateBrandDto createBrandDto)
        {
            string imageName = await _imageService.CreateImageAsync(createBrandDto.Photo);
            createBrandDto.ImageName = imageName;
            await _httpClient.PostAsJsonAsync<CreateBrandDto>("brand", createBrandDto);
        }
        public async Task DeleteBrandAsync(string id)
        {
            await _httpClient.DeleteAsync("brand?id=" + id);
        }
        public async Task<UpdateBrandDto> GetByIdBrandAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("brand/" + id);
            var values = await responseMessage.Content.ReadFromJsonAsync<UpdateBrandDto>();
            return values;
        }
        public async Task<List<ResultBrandDto>> GetAllBrandAsync()
        {
            var responseMessage = await _httpClient.GetAsync("brand");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<ResultBrandDto>>(jsonData);

            foreach (var item in values)
            {
                item.ImageLink = await _imageService.GetImageLinkAsync(item.ImageName);
            }

            return values;
        }
        public async Task UpdateBrandAsync(UpdateBrandDto updateBrandDto)
        {
            if(updateBrandDto.Photo != null)
            {
                string imageName = await _imageService.CreateImageAsync(updateBrandDto.Photo);
                updateBrandDto.ImageName = imageName;
            }
            await _httpClient.PutAsJsonAsync<UpdateBrandDto>("brand", updateBrandDto);
        }
    }
}
