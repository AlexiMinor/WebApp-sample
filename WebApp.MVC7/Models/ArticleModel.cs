using Microsoft.AspNetCore.Mvc;

namespace WebApp.MVC7.Models;
//[Bind("Identifier")]
public class ArticleModel
{
    
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Source { get; set; }
    public float? Rate { get; set; }
    public DateTime Date { get; set; }
    public string SourceUrl { get; set; }
    public string Text{ get; set; }
}