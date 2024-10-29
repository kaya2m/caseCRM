using caseCRM.Entities.DTOs;
using caseCRM.Entities.Entity;
using caseCRM.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace caserCRM.WebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("Auth/Register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var user = new User
            {
                username = userDto.username,
                role = userDto.role,
                password = userDto.password,
            };

            var response = await _userService.CreateUserAsync(user, userDto.password);
            return Json(response);
        }
        [HttpPost("Auth/Login")]
        public async Task<JsonResult> Login(UserDto userDto)
        {
            var response = await _userService.AuthenticateAsync(userDto.username, userDto.password);
            return Json(response);
        }

    }
}
