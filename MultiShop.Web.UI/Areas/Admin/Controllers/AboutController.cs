using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.Dto.CatalogDtos.AboutDtos;
using MultiShop.Web.UI.Services.CatalogServices.AboutServices;

namespace MultiShop.Web.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private const string DEFAULT_AREA = "Admin";
        private const string CONTROLLER_NAME = "About";

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService ?? throw new ArgumentNullException(nameof(aboutService));
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                SetBreadcrumb();
                var aboutList = await _aboutService.GetAllAsync();
                return View(aboutList);
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                TempData["Error"] = "Failed to retrieve about items.";
                return RedirectToAction("Index", "Home", new { area = DEFAULT_AREA });
            }
        }

        [HttpGet]
        public IActionResult CreateAbout()
        {
            SetBreadcrumb();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAbout(CreateAboutDto createAboutDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(createAboutDto);
                }

                await _aboutService.CreateAsync(createAboutDto);
                TempData["Success"] = "About item created successfully.";
                return RedirectToAction(nameof(Index), CONTROLLER_NAME, new { area = DEFAULT_AREA });
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                TempData["Error"] = "Failed to create about item.";
                return View(createAboutDto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAbout(string id)
        {
            try
            {
                await _aboutService.DeleteAsync(id);
                TempData["Success"] = "About item deleted successfully.";
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                TempData["Error"] = "Failed to delete about item.";
            }
            return RedirectToAction(nameof(Index), CONTROLLER_NAME, new { area = DEFAULT_AREA });
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAbout(string id)
        {
            try
            {
                SetBreadcrumb();
                var about = await _aboutService.GetByIdAsync(id);
                return View(about);
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                TempData["Error"] = "Failed to retrieve about item.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDto updateAboutDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(updateAboutDto);
                }

                await _aboutService.UpdateAsync(updateAboutDto);
                TempData["Success"] = "About item updated successfully.";
                return RedirectToAction(nameof(Index), CONTROLLER_NAME, new { area = DEFAULT_AREA });
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                TempData["Error"] = "Failed to update about item.";
                return View(updateAboutDto);
            }
        }

        private void SetBreadcrumb()
        {
            ViewBag.v0 = "About Operations";
            ViewBag.v1 = "Home";
            ViewBag.v2 = "About";
            ViewBag.v3 = "About List";
        }
    }
}
