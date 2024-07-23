using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _VendorDefaultViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
