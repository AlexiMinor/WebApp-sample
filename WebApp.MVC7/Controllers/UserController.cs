using Microsoft.AspNetCore.Mvc;

namespace WebApp.MVC7.Controllers
{
    public class UserController : Controller
    {
        private readonly List<string> _users = new List<string>()
        {
            "Vasya",
            "Nevasya",
            "Alice",
            "Bob",
            "John",
            "Joseph"
        };



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

        public IActionResult Cities([FromQuery]string[]? cities)
        {
            return Ok(cities);
        }
    }
}
