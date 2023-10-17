namespace WebApp.Data.Entities;

public class Comment : IBaseEntity
{
    public Guid Id { get; set; }

    public string Text { get; set; }
    public DateTime Date  { get; set; }

    public Guid ArticleId { get; set; }
    public Article Article { get; set; }

    public Guid? ParentCommentId { get; set; }
    public Comment ParentComment { get; set; }

    public List<Comment> ChildComments { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }
}