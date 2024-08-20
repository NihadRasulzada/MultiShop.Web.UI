using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.ContactDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.Web.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiBaseUrl = "http://157.230.105.226:7010/api/Contact/";

        public ContactController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateContactDto createContactDto)
        {
            createContactDto.SendDate = DateTime.UtcNow.AddHours(4);
            createContactDto.IsRead = false;
            var client = _clientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(createContactDto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync($"{ApiBaseUrl}", content).ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while sending the message.");
                return View(createContactDto);
            }
        }
    }
}
