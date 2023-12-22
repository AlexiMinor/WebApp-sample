using Microsoft.AspNetCore.Mvc;

namespace WebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(/*filter*/)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser()
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser()
        {
            return Ok();
        }
    }
}
