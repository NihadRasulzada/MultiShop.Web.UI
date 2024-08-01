using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.Web.Dto.CatalogDtos.CategoryDtos;
using MultiShop.Web.Dto.CatalogDtos.ProductDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7010/api/";
        private const string ViewBagV1 = "Home Page";
        private const string ViewBagV2 = "Categories";
        private const string ViewBagV3 = "Category Lists";
        private const string ViewBagV0 = "Category works";

        public ProductController(IHttpClientFactory clientFactory)
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
                var response = await client.GetAsync($"{ApiBaseUrl}Product").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var products = JsonConvert.DeserializeObject<List<ResultProductDto>>(data);
                    return View(products);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching products.");
                    return View();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View();
        }

        public async Task<IActionResult> ProductsWithCategory()
        {
            SetViewBagValues();

            var client = _clientFactory.CreateClient();
            try
            {
                var response = await client.GetAsync($"{ApiBaseUrl}Product/ProductsWithCategory").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var products = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(data);
                    return View(products);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching products.");
                    return View();

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View();
        }

        public async Task<IActionResult> Create()
        {
            SetViewBagValues();

            var client = _clientFactory.CreateClient();
            try
            {
                var response = await client.GetAsync($"{ApiBaseUrl}Category").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(data);
                    List<SelectListItem> categoryItems = categories.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id
                    }).ToList();
                    ViewBag.Categories = categoryItems;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching categories.");
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
        public async Task<IActionResult> Create(CreateProductDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync($"{ApiBaseUrl}Product", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "Product", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the product.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(model);
        }

        public async Task<IActionResult> Update(string id)
        {
            SetViewBagValues();

            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError(string.Empty, "Invalid product ID.");
                return BadRequest();
            }

            var client = _clientFactory.CreateClient();
            try
            {
                var response = await client.GetAsync($"{ApiBaseUrl}Product/{id}").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var product = JsonConvert.DeserializeObject<UpdateProductDto>(data);

                    // Fetch categories
                    var categoryResponse = await client.GetAsync($"{ApiBaseUrl}Category").ConfigureAwait(false);
                    if (categoryResponse.IsSuccessStatusCode)
                    {
                        var categoryData = await categoryResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var categories = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(categoryData);
                        List<SelectListItem> categoryItems = categories.Select(x => new SelectListItem
                        {
                            Text = x.Name,
                            Value = x.Id
                        }).ToList();
                        ViewBag.Categories = categoryItems;
                    }

                    return View(product);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching product details.");
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
        public async Task<IActionResult> Update(string id, UpdateProductDto model)
        {
            SetViewBagValues();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync($"{ApiBaseUrl}Product", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "Product", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the product.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return View();

            }

            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError(string.Empty, "Invalid product ID.");
                return BadRequest();
            }

            var client = _clientFactory.CreateClient();

            try
            {
                var response = await client.DeleteAsync($"{ApiBaseUrl}Product/?id=" + id).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "Product", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the product.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return BadRequest();
        }
    }
}
