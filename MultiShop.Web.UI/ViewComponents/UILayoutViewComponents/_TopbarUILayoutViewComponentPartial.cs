using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.ViewComponents.UILayoutViewComponents
{
    public class _TopbarUILayoutViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
