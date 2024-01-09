using Microsoft.AspNetCore.Mvc;
using WebApp.Core;
using WebApp.Mappers;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService, UserMapper userMapper, IUserService userService)
        {
            _tokenService = tokenService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken(LoginModel request)
        {
            //can be refactored
            var isUserCorrect = await _userService.CheckPassword(request.Email, request.Password);
            if (isUserCorrect)
            {

                var userDto = await _userService.GetUserByEmail(request.Email);
                var jwtToken = await _tokenService.GenerateJwtToken(userDto);
                var refreshToken = await _tokenService.AddRefreshToken(userDto.Email,
                    HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                return Ok(new TokenResponseModel { AccessToken = jwtToken, RefreshToken = refreshToken });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("Refresh")]
        public async Task<IActionResult> GenerateToken(RefreshTokenModel request)
        {
            //can be refactored
            var isRefreshTokenValid = await _tokenService.CheckRefreshToken(request.RefreshToken);
            if (isRefreshTokenValid)
            {
                var userDto = await _userService.GetUserByRefreshToken(request.RefreshToken);
                var jwtToken = await _tokenService.GenerateJwtToken(userDto);
                var refreshToken = await _tokenService.AddRefreshToken(userDto.Email,
                    HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString());
                await _tokenService.RemoveRefreshToken(request.RefreshToken);
                return Ok(new TokenResponseModel { AccessToken = jwtToken, RefreshToken = refreshToken });
            }

            return Unauthorized();
        }

        [HttpDelete]
        [Route("Revoke")]
        public async Task<IActionResult> RevokeToken(RefreshTokenModel request)
        {
            //todo check if exists to return correct status code
            //todo check that RT for same user as from request
            await _tokenService.RemoveRefreshToken(request.RefreshToken);
            return Ok();
        }


        //private async Task<TokenResponseModel> GetTokenResponse(UserDto userDto)
        //{
           
        //}
    }
}
