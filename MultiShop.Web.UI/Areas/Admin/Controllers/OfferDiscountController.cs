using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.OfferDiscountDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class OfferDiscountController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7010/api/OfferDiscount/";
        private const string ViewBagV1 = "Home Page";
        private const string ViewBagV2 = "OfferDiscounts";
        private const string ViewBagV3 = "OfferDiscount Lists";
        private const string ViewBagV0 = "OfferDiscount works";

        public OfferDiscountController(IHttpClientFactory clientFactory)
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
                    var OfferDiscounts = JsonConvert.DeserializeObject<List<ResultOfferDiscountDto>>(data);
                    return View(OfferDiscounts);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching OfferDiscounts.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(new List<ResultOfferDiscountDto>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            SetViewBagValues();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOfferDiscountDto createOfferDiscountDto)
        {
            SetViewBagValues();

            if (!ModelState.IsValid)
            {
                return View(createOfferDiscountDto);
            }

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createOfferDiscountDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync($"{ApiBaseUrl}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "OfferDiscount", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the OfferDiscount.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(createOfferDiscountDto);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ModelState.AddModelError(string.Empty, "Invalid OfferDiscount ID.");
                return BadRequest();
            }

            var client = _clientFactory.CreateClient();

            try
            {
                var response = await client.DeleteAsync($"{ApiBaseUrl}?id=" + id).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "OfferDiscount", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while deleting the OfferDiscount.");
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
                    var OfferDiscount = JsonConvert.DeserializeObject<UpdateOfferDiscountDto>(data);
                    return View(OfferDiscount);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while fetching OfferDiscount details.");
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
        public async Task<IActionResult> Update(string id, UpdateOfferDiscountDto updateOfferDiscountDto)
        {
            if (!ModelState.IsValid)
            {
                return View(updateOfferDiscountDto);
            }

            var client = _clientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateOfferDiscountDto);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PutAsync($"{ApiBaseUrl}", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index), "OfferDiscount", new { area = nameof(Admin) });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the OfferDiscount.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }

            return View(updateOfferDiscountDto);
        }
    }
}
