namespace WebApp.Core;

public class ArticleDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Text { get; set; }
    public DateTime Date { get; set; }
    public float? Rate { get; set; }
    public string SourceUrl { get; set; }
    public Guid ArticleSourceId { get; set; }
}