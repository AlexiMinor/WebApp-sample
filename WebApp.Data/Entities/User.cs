namespace WebApp.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public List<Comment> Comments { get; set; }
}