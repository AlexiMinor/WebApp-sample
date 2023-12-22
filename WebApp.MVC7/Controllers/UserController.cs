using System.Security.Claims;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core;
using WebApp.Data.Entities;
using WebApp.MVC7.Models;
using WebApp.Services.Interfaces;

namespace WebApp.MVC7.Controllers
{
    public class UserController : Controller
    {
        private readonly IValidator<UserRegisterModel> _registerValidator;
        private readonly IValidator<UserLoginModel> _loginValidator;
        private readonly IUserService _userService;
        private readonly List<string> _users = new List<string>()
        {
            "Vasya",
            "Nevasya",
            "Alice",
            "Bob",
            "John",
            "Joseph"
        };

        public UserController(IValidator<UserRegisterModel> registerValidator, IUserService userService, IValidator<UserLoginModel> loginValidator)
        {
            _registerValidator = registerValidator;
            _userService = userService;
            _loginValidator = loginValidator;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            var result = await _registerValidator.ValidateAsync(model);
            if (result.IsValid && !_userService.IsUserExists(model.Email))
            {
                var dto = new UserDto()
                {
                    Email = model.Email,
                    Password = model.Password
                };
                await _userService.RegisterUser(dto);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(await _userService.Authenticate(dto.Email)));
                return Ok(model);
            }
            result.AddToModelState(ModelState);
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]UserLoginModel model)
        {
            var x = await new StreamReader(Request.Body).ReadToEndAsync();
            var result = await _loginValidator.ValidateAsync(model);
            if (result.IsValid && _userService.IsUserExists(model.Email))
            {
                if (await _userService.IsPasswordCorrect(model.Email, model.Password))
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(await _userService.Authenticate(model.Email)));
                }
                return RedirectToAction("Index", "Home");
            }
            result.AddToModelState(ModelState);
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Test()
        {
            return Ok("Hello there");
        }

        [HttpGet]
        public IActionResult CheckEmail(string email)
        {
            if (email.StartsWith("a", StringComparison.InvariantCultureIgnoreCase))
            {
                return Json(false);
            }
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
