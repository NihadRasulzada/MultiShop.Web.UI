using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.CommentServices;
using MultiShop.Web.UI.Services.Interfaces;
using MultiShop.Web.UI.Services.MessageService;

namespace MultiShop.Web.UI.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutHeaderComponentPartial : ViewComponent
    {
        private readonly IMessageService _messageService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        public _AdminLayoutHeaderComponentPartial(IMessageService messageService, IUserService userService, ICommentService commentService)
        {
            _messageService = messageService;
            _userService = userService;
            _commentService = commentService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userService.GetUserInfo();
            int messageCount = await _messageService.GetTotalMessageCountByReceiverIdAsync(user.Id);
            ViewBag.messageCount = messageCount;

            int totalCommentcount = await _commentService.GetTotalCommentCount();
            ViewBag.totalCommentCount = totalCommentcount;

            return View();
        }
    }
}
