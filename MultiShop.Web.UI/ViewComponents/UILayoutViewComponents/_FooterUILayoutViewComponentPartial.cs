using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
