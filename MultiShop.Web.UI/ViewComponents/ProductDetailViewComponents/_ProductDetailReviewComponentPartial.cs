using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CommentDtos;
using MultiShop.Web.UI.Services.CatalogServices.ProductDetailServices;
using MultiShop.Web.UI.Services.CommentServices;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailReviewComponentPartial : ViewComponent
    {
        private readonly ICommentService _commentService;

        public _ProductDetailReviewComponentPartial(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            List<ResultCommentDto> values = await _commentService.CommentListByProductId(productId);
            return View(values);
        }
    }
}
