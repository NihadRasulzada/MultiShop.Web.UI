using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.CategoryDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7010/api/Category/";
        private const string ViewBagV1 = "Home Page";
        private const string ViewBagV2 = "Categories";
        private const string ViewBagV3 = "Category Lists";
        private const string ViewBagV0 = "Category works";

        public CategoryController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private void SetViewBagValues()
        {
            ViewBag.v1 = ViewBagV1;
            ViewBag.v2 = ViewBagV2;
            ViewBag.v3 = ViewBagV3;
            ViewBag.v0 = ViewBagV0;
        }

        public async Task<IActionResult> Index()
        {
            SetViewBagValues();

            var client = _clientFactory.CreateClient();
            try
            {
                var response = await client.GetAsync($"{ApiBaseUrl}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(data);
                    return View(categories);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching categories.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(new List<ResultCategoryDto>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewBagValues();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto createCategoryDto)
        {
            SetViewBagValues();

            if (!ModelState.IsValid)
            {
                return View(createCategoryDto);
            }

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCategoryDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync($"{ApiBaseUrl}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "Category", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the category.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(createCategoryDto);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError(string.Empty, "Invalid category ID.");
                return BadRequest();
            }

            var client = _clientFactory.CreateClient();

            try
            {
                var response = await client.DeleteAsync($"{ApiBaseUrl}?id=" + id).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "Category", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the category.");
                    return View();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return BadRequest();
        }

        public async Task<IActionResult> Update(string id)
        {
            SetViewBagValues();

            var client = _clientFactory.CreateClient();
            try
            {
                var response = await client.GetAsync($"{ApiBaseUrl}{id}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var category = JsonConvert.DeserializeObject<UpdateCategoryDto>(data);
                    return View(category);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching category details.");
                    return View();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UpdateCategoryDto updateCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateCategoryDto);
            }

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCategoryDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync($"{ApiBaseUrl}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "Category", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the category.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(updateCategoryDto);
        }
    }
}
