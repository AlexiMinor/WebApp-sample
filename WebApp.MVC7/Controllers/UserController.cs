using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.Entities;
using WebApp.MVC7.Models;

namespace WebApp.MVC7.Controllers
{
    public class UserController : Controller
    {
        private readonly IValidator<UserRegisterModel> _registerValidator;
        private readonly List<string> _users = new List<string>()
        {
            "Vasya",
            "Nevasya",
            "Alice",
            "Bob",
            "John",
            "Joseph"
        };

        public UserController(IValidator<UserRegisterModel> registerValidator)
        {
            _registerValidator = registerValidator;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new UserRegisterModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            var result = await _registerValidator.ValidateAsync(model);
            if (result.IsValid)
            {
                return Ok(model);
            }
            result.AddToModelState(ModelState);
            return View(model);
        }

        //domain/controller/action ? search = J & age = 39
        public IActionResult Search(string? search, int? age)
        {
            if (age<18)
            {
                return Unauthorized();
            }

            if (string.IsNullOrWhiteSpace(search))
            {
                return Ok(_users);
            }
            var searchedUsers = _users.Where(u => u.StartsWith(search));
            return Ok(searchedUsers);
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
    }
}
