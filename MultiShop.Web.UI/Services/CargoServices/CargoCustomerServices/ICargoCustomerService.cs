using MultiShop.Web.Dto.CargoDtos.CargoCustomerDtos;

namespace MultiShop.Web.UI.Services.CargoServices.CargoCustomerServices
{
    public interface ICargoCustomerService
    {
        Task<GetCargoCustomerByIdDto> GetByIdCargoCustomerInfoAsync(string id);
    }
}
