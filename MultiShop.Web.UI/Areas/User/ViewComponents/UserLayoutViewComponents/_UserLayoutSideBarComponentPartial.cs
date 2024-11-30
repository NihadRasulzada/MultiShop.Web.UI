using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.User.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutSideBarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
