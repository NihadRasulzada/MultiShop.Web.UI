﻿using Microsoft.AspNetCore.Mvc;
using MultiShop.Web.UI.Services.Interfaces;

namespace MultiShop.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var values = await _userService.GetUserInfo();
            return View(values);
        }
    }
}
