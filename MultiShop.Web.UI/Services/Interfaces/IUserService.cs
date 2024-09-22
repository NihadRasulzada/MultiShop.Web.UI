using MultiShop.Web.UI.Models;

namespace MultiShop.Web.UI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDetailViewModel> GetUserInfo();
    }
}
