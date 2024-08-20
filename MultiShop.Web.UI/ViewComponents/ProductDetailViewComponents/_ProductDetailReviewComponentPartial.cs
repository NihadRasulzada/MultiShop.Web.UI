using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CommentDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.ViewComponents.ProductDetailViewComponents
{
    public class _ProductDetailReviewComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7070/api/Comment/";

        public _ProductDetailReviewComponentPartial(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(string productId)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync($"{ApiBaseUrl}CommentListByProductId?productId=" + productId).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var categories = JsonConvert.DeserializeObject<List<ResultCommentDto>>(data);
                return View(categories);
            }

            return View(new List<ResultCommentDto>());
        }
    }
}
