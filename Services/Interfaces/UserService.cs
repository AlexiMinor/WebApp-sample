using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApp.Core;
using WebApp.Data.Entities;
using WebApp.Repositories;

namespace WebApp.Services.Interfaces;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<int> RegisterUser(UserDto userDto)
    {
        var userRole = await _unitOfWork.RoleRepository.FindBy(role => role.Name.Equals("User")).FirstOrDefaultAsync();
        var user = new User()
        {
            Id = Guid.NewGuid(),
            UserName = userDto.Email,
            PasswordHash = GenerateMd5Hash(userDto.Password),
            RoleId = userRole.Id
        };
        await _unitOfWork.UserRepository.InsertOne(user);

        return await _unitOfWork.Commit(); 
    }

    public bool IsUserExists(string email)
    {
        return _unitOfWork.UserRepository.FindBy(user => user.UserName.Equals(email)).Any();
    }

    public async Task<ClaimsIdentity> Authenticate(string userName)
    {
        var user = await _unitOfWork.UserRepository
            .FindBy(us 
                => us.UserName.Equals(userName))
            .FirstOrDefaultAsync();
        if (user != null)
        {
            var roleName = (await _unitOfWork.RoleRepository.GetByIdAsNoTracking(user.RoleId)).Name;

            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)

            };


            var claimsIdentity =
                new ClaimsIdentity(claims,
                    "ApplicationCookie",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        return null;
    }

    public async Task<bool> IsPasswordCorrect(string email, string password)
    {
        var currentPasswordHash = (await _unitOfWork.UserRepository
            .FindBy(user => user.UserName.Equals(email))
            .FirstOrDefaultAsync())?
                .PasswordHash;

        var enteredPasswordHash = GenerateMd5Hash(password);

        return currentPasswordHash?.Equals(enteredPasswordHash) ?? false;
    }

    public async Task<bool> IsAdmin(string email)
    {
        return false;
        //
    }

    private string GenerateMd5Hash(string input)
    {
        using (var md5 = MD5.Create())
        {
            var salt = _configuration["AppSettings:PasswordSalt"];
            var inputBytes = System.Text.Encoding.UTF8.GetBytes($"{input}{salt}");
            var hashBytes = md5.ComputeHash(inputBytes);
            
            return Convert.ToHexString(hashBytes);
        }


    }
}