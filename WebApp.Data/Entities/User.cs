namespace WebApp.Data.Entities;

public class User : IBaseEntity
{
    public Guid Id { get; set; }
    public string UserName { get; set; }//email
    public string PasswordHash { get; set; }
    public List<Comment> Comments { get; set; }

    public Guid RoleId { get; set; }
    public Role Role{ get; set; }
}