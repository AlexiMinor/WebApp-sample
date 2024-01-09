using System.Security.Claims;
using System.Security.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApp.Core;
using WebApp.Data.CQS.Queries;
using WebApp.Data.Entities;
using WebApp.Mappers;
using WebApp.Repositories;

namespace WebApp.Services.Interfaces;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserMapper _userMapper;
    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;

    public UserService(IUnitOfWork unitOfWork, IConfiguration configuration, IMediator mediator, UserMapper userMapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _mediator = mediator;
        _userMapper = userMapper;
    }

    public async Task<int> RegisterUser(UserDto userDto)
    {
        var userRole = await _unitOfWork.RoleRepository.FindBy(role => role.Name.Equals("User")).FirstOrDefaultAsync();
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Email = userDto.Email,
            PasswordHash = GenerateMd5Hash(userDto.Password),
            RoleId = userRole.Id
        };
        await _unitOfWork.UserRepository.InsertOne(user);

        return await _unitOfWork.Commit();
    }

    public bool IsUserExists(string email)
    {
        return _unitOfWork.UserRepository.FindBy(user => user.Email.Equals(email)).Any();
    }

    public async Task<ClaimsIdentity> Authenticate(string userName)
    {
        var user = await _unitOfWork.UserRepository
            .FindBy(us
                => us.Email.Equals(userName))
            .FirstOrDefaultAsync();
        if (user != null)
        {
            var roleName = (await _unitOfWork.RoleRepository.GetByIdAsNoTracking(user.RoleId)).Name;

            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
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

    public async Task<bool> CheckPassword(string email, string password)
    {
        var currentPasswordHash = (await _unitOfWork.UserRepository
            .FindBy(user => user.Email.Equals(email))
            .FirstOrDefaultAsync())?
                .PasswordHash;

        var enteredPasswordHash = GenerateMd5Hash(password);

        return currentPasswordHash?.Equals(enteredPasswordHash) ?? false;
    }

    public async Task<UserDto> GetUserByEmail(string userDtoEmail)
    {
        var query = new GetUserByEmailQuery() { Email = userDtoEmail };
        var user = await _mediator.Send(query);

        return _userMapper.UserToUserDto(user);
    }

    public async Task<UserDto> GetUserByRefreshToken(Guid refreshToken)
    {
        var user = await _mediator.Send(new GetUserByRefreshTokenQuery { RefreshTokenId = refreshToken });

        var dto = _userMapper.UserToUserDto(user);
        return dto;
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