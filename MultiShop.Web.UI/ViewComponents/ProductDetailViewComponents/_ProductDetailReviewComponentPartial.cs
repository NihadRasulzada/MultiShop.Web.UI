using Microsoft.AspNetCore.Mvc;

namespace MultiShop.Web.UI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailReviewComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke( )
        {
            return View();
        }
    }
}
