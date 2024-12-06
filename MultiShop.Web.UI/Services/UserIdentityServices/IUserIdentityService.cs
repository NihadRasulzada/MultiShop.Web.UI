using MultiShop.Web.Dto.AuthDtos.UserDtos;

namespace MultiShop.Web.UI.Services.UserIdentityServices
{
    public interface IUserIdentityService
    {
        Task<List<ResultUserDto>> GetAllUserListAsync();
    }
}
