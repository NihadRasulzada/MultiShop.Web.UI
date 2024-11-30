using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.Areas.User.ViewComponents.UserLayoutViewComponents
{
    public class _UserLayoutHeadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
