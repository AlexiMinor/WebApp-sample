using Microsoft.AspNetCore.Identity;

namespace WebApp.MvcIdentity.Data;

public class MyCustomUser : IdentityUser<Guid>//, IBaseEntity
{
    public int Age { get; set; }
}

public class MyCustomRole : IdentityRole<Guid>//, IBaseEntity
{
}