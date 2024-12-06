using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Models;
using MultiShop.Web.UI.Services.Interfaces;
using MultiShop.Web.UI.Services.MessageService;

namespace MultiShop.Web.UI.Areas.User.Controllers
{
    [Area(nameof(User))]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;

        public MessageController(IMessageService messageService, IUserService userService)
        {
            _messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        public async Task<IActionResult> Inbox()
        {
            var user = await GetUserAsync();
            if (user == null)
            {
                TempData["Error"] = "User information could not be retrieved.";
                return RedirectToAction("Login", "Account"); // Yönləndirməni uyğun görün.
            }

            try
            {
                var values = await _messageService.GetInboxMessageAsync(user.Id);
                return View(values);
            }
            catch (Exception ex)
            {
                // Loqlaşdırma
                Console.WriteLine($"Error fetching inbox messages: {ex.Message}");
                TempData["Error"] = "An error occurred while fetching inbox messages.";
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<IActionResult> Sendbox()
        {
            var user = await GetUserAsync();
            if (user == null)
            {
                TempData["Error"] = "User information could not be retrieved.";
                return RedirectToAction("Login", "Account");
            }

            try
            {
                var values = await _messageService.GetSendboxMessageAsync(user.Id);
                return View(values);
            }
            catch (Exception ex)
            {
                // Loqlaşdırma
                Console.WriteLine($"Error fetching sendbox messages: {ex.Message}");
                TempData["Error"] = "An error occurred while fetching sendbox messages.";
                return RedirectToAction("Error", "Home");
            }
        }

        private async Task<UserDetailViewModel> GetUserAsync()
        {
            try
            {
                return await _userService.GetUserInfo();
            }
            catch (Exception ex)
            {
                // Loqlaşdırma
                Console.WriteLine($"Error retrieving user information: {ex.Message}");
                return null;
            }
        }
    }
}
