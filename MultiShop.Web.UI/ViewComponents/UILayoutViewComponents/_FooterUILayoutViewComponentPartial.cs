using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CatalogServices.AboutServices;

namespace MultiShop.Web.UI.ViewComponents.UILayoutViewComponents
{
    public class _FooterUILayoutViewComponentPartial : ViewComponent
    {
        private readonly IAboutService _aboutService;
        public _FooterUILayoutViewComponentPartial(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _aboutService.GetAllAsync();
            return View(values);
        }
    }
}
