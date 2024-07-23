using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _FeaturedDefaultViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
