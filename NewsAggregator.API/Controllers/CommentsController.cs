using Microsoft.AspNetCore.Mvc;

namespace WebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(/*filter*/)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment()
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateComment()
        {
            return Ok();
        }
    }
}
