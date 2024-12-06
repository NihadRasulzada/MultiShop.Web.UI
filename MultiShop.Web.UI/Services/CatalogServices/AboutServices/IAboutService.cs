using MultiShop.Web.Dto.CatalogDtos.AboutDtos;

namespace MultiShop.Web.UI.Services.CatalogServices.AboutServices
{
    public interface IAboutService
    {
        Task<List<ResultAboutDto>> GetAllAsync();
        Task<bool> CreateAsync(CreateAboutDto createAboutDto);
        Task<bool> UpdateAsync(UpdateAboutDto updateAboutDto);
        Task<bool> DeleteAsync(string id);
        Task<UpdateAboutDto> GetByIdAsync(string id);
    }
}
