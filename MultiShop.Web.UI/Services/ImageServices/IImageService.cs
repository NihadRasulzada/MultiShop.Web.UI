using MultiShop.Web.Dto.CatalogDtos.BrandsDto;

namespace MultiShop.Web.UI.Services.ImageServices
{
    public interface IImageService
    {
        Task<string> CreateImageAsync(IFormFile photo);
        Task DeleteImageAsync(string imageName);
        Task<string> GetImageLinkAsync(string imageName);
    }
}
