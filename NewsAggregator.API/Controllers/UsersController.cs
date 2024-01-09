using Microsoft.AspNetCore.Mvc;
using WebApp.Mappers;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserMapper _userMapper;
        private readonly ITokenService _tokenService;

        public UsersController(IUserService userService, 
            UserMapper userMapper, ITokenService tokenService)
        {
            _userService = userService;
            _userMapper = userMapper;
            _tokenService = tokenService;
        }

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
        public async Task<IActionResult> CreateUser(RegisterModel request)
        {
            //validation

            var userDto = _userMapper.RegisterModelToUserDto(request);
            await _userService.RegisterUser(userDto);

            var user = await _userService.GetUserByEmail(userDto.Email);

            //var token = await _tokenService.GenerateJwtToken(user);

            return Created($"users/{user.Id}", null);
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
