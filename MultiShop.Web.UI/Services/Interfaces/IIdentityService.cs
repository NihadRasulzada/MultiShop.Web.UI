using MultiShop.Web.Dto.AuthDtos.LoginDtos;

namespace MultiShop.Web.UI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<bool> SignIn(SignInDto signInDto);
        Task<bool> GetRefreshToken();
    }
}
