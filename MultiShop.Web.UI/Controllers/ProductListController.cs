using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using MultiShop.Web.Dto.CommentDtos;
using Newtonsoft.Json;

namespace MultiShop.Web.UI.Controllers
{
    public class ProductListController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7070/api/Comment/";

        public ProductListController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index(string id)
        {
            ViewBag.CategoryId = id;
            return View();
        }

        public IActionResult ProductDetail(string id)
        {
            ViewBag.ProductId = id;
            return View();
        }

        public async Task<PartialViewResult> AddComment()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult AddComment(CreateCommentDto createCommentDto)
        {
            return RedirectToAction("Index", "Default");
        }
    }
}
