using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MultiShop.Web.Dto.CatalogDtos.CategoryDtos;
using System.Text;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public CategoryController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Home Page";
            ViewBag.v2 = "Categories";
            ViewBag.v3 = "Category Lists";
            ViewBag.v0 = "Category works";

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://157.230.105.226:7010/api/Category");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(data);
                return View(categories);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.v1 = "Home Page";
            ViewBag.v2 = "Categories";
            ViewBag.v3 = "Category Lists";
            ViewBag.v0 = "Category works";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
        {
            ViewBag.v1 = "Home Page";
            ViewBag.v2 = "Categories";
            ViewBag.v3 = "Category Lists";
            ViewBag.v0 = "Category works";

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("http://157.230.105.226:7010/api/Category", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index), "Category", new { Areas = nameof(Admin) });
            }
            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            var client = _clientFactory.CreateClient();
            var response = await client.DeleteAsync("http://157.230.105.226:7010/api/Category/?id=" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index), "Category", new { Areas = nameof(Admin) });
            }
            return BadRequest();
        }


        public async Task<IActionResult> Update(string id)
        {
            ViewBag.v1 = "Home Page";
            ViewBag.v2 = "Categories";
            ViewBag.v3 = "Category Lists";
            ViewBag.v0 = "Category works";

            var client = _clientFactory.CreateClient();
            var response = await client.GetAsync("http://157.230.105.226:7010/api/Category/" + id);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var category = JsonConvert.DeserializeObject<UpdateCategoryDto>(data);
                return View(category);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UpdateCategoryDto updateCategoryDto)
        {
            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://157.230.105.226:7010/api/Category/", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index), "Category", new { Areas = nameof(Admin) });
            }
            return View();
        }
    }
}
