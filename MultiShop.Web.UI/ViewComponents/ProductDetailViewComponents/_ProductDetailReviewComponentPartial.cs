using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CommentDtos;
using MultiShop.Web.UI.Services.CatalogServices.ProductDetailServices;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailReviewComponentPartial : ViewComponent
    {
        private readonly IProductDetailService _productDetailService;

        public _ProductDetailReviewComponentPartial(IProductDetailService productDetailService)
        {
            _productDetailService = productDetailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {

        }
    }
}
