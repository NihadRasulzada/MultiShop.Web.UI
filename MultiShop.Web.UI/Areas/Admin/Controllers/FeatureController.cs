using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.FeatureDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class FeatureController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7010/api/Feature/";
        private const string ViewBagV1 = "Home Page";
        private const string ViewBagV2 = "Features";
        private const string ViewBagV3 = "Feature Lists";
        private const string ViewBagV0 = "Feature works";

        public FeatureController(IHttpClientFactory clientFactory)
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
                    var Features = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(data);
                    return View(Features);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching Features.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(new List<ResultFeatureDto>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewBagValues();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFeatureDto createFeatureDto)
        {
            SetViewBagValues();

            if (!ModelState.IsValid)
            {
                return View(createFeatureDto);
            }

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFeatureDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync($"{ApiBaseUrl}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "Feature", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the Feature.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(createFeatureDto);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError(string.Empty, "Invalid Feature ID.");
                return BadRequest();
            }

            var client = _clientFactory.CreateClient();

            try
            {
                var response = await client.DeleteAsync($"{ApiBaseUrl}?id=" + id).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "Feature", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the Feature.");
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
                    var Feature = JsonConvert.DeserializeObject<UpdateFeatureDto>(data);
                    return View(Feature);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching Feature details.");
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
        public async Task<IActionResult> Update(string id, UpdateFeatureDto updateFeatureDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateFeatureDto);
            }

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync($"{ApiBaseUrl}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "Feature", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the Feature.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(updateFeatureDto);
        }
    }
}
