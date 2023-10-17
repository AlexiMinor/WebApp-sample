namespace WebApp.Data.Entities;

public interface IBaseEntity 
{
    public Guid Id { get; set; }
}

//public interface IBaseEntity<T>
//{
//    public T Id { get; set; }
//}