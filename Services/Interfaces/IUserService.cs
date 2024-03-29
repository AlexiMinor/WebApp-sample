﻿using System.Security.Claims;
using WebApp.Core;

namespace WebApp.Services.Interfaces;

public interface IUserService
{
    public Task<int> RegisterUser(UserDto userDto);
    bool IsUserExists(string email);
    Task<bool> IsAdmin(string email);
    public Task<ClaimsIdentity> Authenticate(string userName);
    public  Task<bool> CheckPassword(string email, string password);
    Task<UserDto> GetUserByEmail(string userDtoEmail);
    Task<UserDto> GetUserByRefreshToken(Guid refreshToken);
}