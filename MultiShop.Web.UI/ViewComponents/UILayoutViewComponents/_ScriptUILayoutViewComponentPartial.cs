using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.ViewComponents.UILayoutViewComponents
{
    public class _ScriptUILayoutViewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
