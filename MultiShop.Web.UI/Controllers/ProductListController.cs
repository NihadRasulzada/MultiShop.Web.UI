using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CommentDtos;

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

            ViewBag.directory1 = "Home";
            ViewBag.directory2 = "Shop";
            ViewBag.directory2 = "Shop List";

            return View();
        }

        public IActionResult ProductDetail(string id)
        {
            ViewBag.ProductId = id;

            ViewBag.directory1 = "Home";
            ViewBag.directory2 = "Shop";
            ViewBag.directory2 = "Shop Detail";

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
