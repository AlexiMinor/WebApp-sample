using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApp.Core;
using WebApp.Data.CQS.Commands;
using WebApp.Data.CQS.Queries;
using WebApp.Services.Interfaces;

namespace WebApp.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public TokenService(IConfiguration configuration, IMediator mediator)
    {
        _configuration = configuration;
        _mediator = mediator;
    }

    public async Task<string> GenerateJwtToken(UserDto userDto)
    {
        var isLifetime = int.TryParse(_configuration["Jwt:Lifetime"], out var lifetime);
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]);
        var iss = _configuration["Jwt:Issuer"];
        var aud = _configuration["Jwt:Audience"];

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.Role, "Admin"),//todo add correct role
                new Claim("aud",aud),
                new Claim("iss",iss)
                //...
            }),
            Expires = DateTime.UtcNow.AddMinutes(lifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public async Task<Guid> AddRefreshToken(string requestEmail, string ipAddress)
    {
        var user = await _mediator.Send(new GetUserByEmailQuery() { Email = requestEmail });
        //todo change to be more correct from CQRS point of view
        var refreshToken = await _mediator.Send(new AddRefreshTokenCommand() { UserId = user.Id, Ip = ipAddress });
        return refreshToken;
    }

    public async Task<bool> CheckRefreshToken(Guid requestRefreshToken)
    {
        var rt = await _mediator.Send(new GetRefreshTokenQuery { Id = requestRefreshToken });
        if (rt != null && rt.ExpiringAt.ToUniversalTime() < DateTime.UtcNow)
        {
            return true;
        }

        return false;
    }

    public async Task RemoveRefreshToken(Guid requestRefreshToken)
    {
        await _mediator.Send(new DeleteRefreshTokenCommand()
        {
            Id = requestRefreshToken
        });
    }
}