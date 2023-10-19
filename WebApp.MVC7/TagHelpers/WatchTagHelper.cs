using Microsoft.AspNetCore.Razor.TagHelpers;
using WebApp.Repositories;

namespace WebApp.MVC7.TagHelpers;


public class WatchTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Content.SetContent($"Current time: {DateTime.Now:T}");
    }
}

public class DateTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "div";
        output.Content.SetContent($"Current date: {DateTime.Now:D}");
    }
}

public class DateTimeTagHelper : TagHelper
{
    
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var id = context.AllAttributes["id"];
        output.TagName = "div";
        var target = await output.GetChildContentAsync();
        var content = $"<h2>Watch Info {id.Value}:</h2>{target.GetContent()}";
        output.Content.SetHtmlContent(content);
    }
}
