using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.CategoryDtos;
using MultiShop.Web.UI.Services.CatalogServices.CategoryServices;
using Newtonsoft.Json;

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
