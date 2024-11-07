using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.FeatureDtos;
using MultiShop.Web.UI.Services.CatalogServices.FeatureServices;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _FeaturedDefaultViewComponentPartial : ViewComponent
    {
        private readonly IFeatureService _featureService;
        public _FeaturedDefaultViewComponentPartial(IFeatureService featureService)
        {
            _featureService = featureService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _featureService.GetAllFeatureAsync();
            return View(values);
        }
    }
}
