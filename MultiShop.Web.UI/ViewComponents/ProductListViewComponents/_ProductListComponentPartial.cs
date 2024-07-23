using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.ViewComponents.ProductListViewComponents
{
    public class _ProductListComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
