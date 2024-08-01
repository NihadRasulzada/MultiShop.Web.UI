using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.FeatureSliderDto;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class FeatureSliderController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7010/api/FeatureSlider/";
        private const string ViewBagV1 = "Home Page";
        private const string ViewBagV2 = "FeatureSliders";
        private const string ViewBagV3 = "FeatureSlider Lists";
        private const string ViewBagV0 = "FeatureSlider works";

        public FeatureSliderController(IHttpClientFactory clientFactory)
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
                    string data = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<ResultFeatureSliderDto> featureSliderDtos = JsonConvert.DeserializeObject<List<ResultFeatureSliderDto>>(data);
                    return View(featureSliderDtos);
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


            return View(new List<ResultFeatureSliderDto>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewBagValues();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFeatureSliderDto createFeatureSliderDto)
        {
            SetViewBagValues();

            if (!ModelState.IsValid)
            {
                return View(createFeatureSliderDto);
            }

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createFeatureSliderDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync($"{ApiBaseUrl}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "FeatureSlider", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the FeatureSlider.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(createFeatureSliderDto);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError(string.Empty, "Invalid FeatureSlider ID.");
                return BadRequest();
            }

            var client = _clientFactory.CreateClient();

            try
            {
                var response = await client.DeleteAsync($"{ApiBaseUrl}?id=" + id).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "FeatureSlider", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the FeatureSlider.");
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
                    var FeatureSlider = JsonConvert.DeserializeObject<UpdateFeatureSliderDto>(data);
                    return View(FeatureSlider);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching FeatureSlider details.");
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
        public async Task<IActionResult> Update(string id, UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateFeatureSliderDto);
            }

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateFeatureSliderDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync($"{ApiBaseUrl}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "FeatureSlider", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the FeatureSlider.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(updateFeatureSliderDto);
        }
    }
}
