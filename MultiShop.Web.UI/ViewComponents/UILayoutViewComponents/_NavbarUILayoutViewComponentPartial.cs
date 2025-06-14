﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CatalogServices.CategoryServices;

namespace MultiShop.Web.UI.ViewComponents.UILayoutViewComponents
{
    public class _NavbarUILayoutViewComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public _NavbarUILayoutViewComponentPartial(ICategoryService categoryService)
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
