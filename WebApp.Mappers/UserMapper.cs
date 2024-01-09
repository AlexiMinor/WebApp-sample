using Riok.Mapperly.Abstractions;
using WebApp.Core;
using WebApp.Data.Entities;
using WebApp.Models;

namespace WebApp.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial UserDto RegisterModelToUserDto(RegisterModel model);
    public partial UserDto LoginModeToUserDto(LoginModel model);
    public partial UserDto UserToUserDto(User model);
}