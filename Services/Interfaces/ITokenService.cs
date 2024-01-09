using System.Net;
using WebApp.Core;

namespace WebApp.Services.Interfaces;

public interface ITokenService
{
    public Task<string> GenerateJwtToken(UserDto userDto);
    public Task<Guid> AddRefreshToken(string requestEmail, string ipAddress);
    Task<bool> CheckRefreshToken(Guid requestRefreshToken);
    Task RemoveRefreshToken(Guid requestRefreshToken);
}