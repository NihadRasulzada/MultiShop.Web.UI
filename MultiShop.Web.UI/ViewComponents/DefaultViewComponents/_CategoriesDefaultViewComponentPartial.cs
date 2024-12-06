using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CatalogServices.CategoryServices;

namespace MultiShop.Web.UI.ViewComponents.DefaultViewComponents
{
    public class _CategoriesDefaultViewComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public _CategoriesDefaultViewComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var values = await _categoryService.GetAllCategoryAsync();
            return View(values);
        }
    }
}
