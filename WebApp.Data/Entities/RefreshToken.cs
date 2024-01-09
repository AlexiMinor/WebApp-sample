namespace WebApp.Data.Entities;

public class RefreshToken : IBaseEntity
{
    public Guid Id { get; set; }

    public DateTime GeneratedAt { get; set; }
    public DateTime ExpiringAt { get; set; }

    public string AssociatedDeviceName { get; set; }
    //public bool IsRevoked
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}