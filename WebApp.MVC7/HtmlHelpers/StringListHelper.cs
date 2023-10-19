using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.MVC7.HtmlHelpers;

public static class StringListHelper
{
    public static HtmlString CreateList(this IHtmlHelper htmlHelper, string[] items)
    {
        var result = new StringBuilder("<ul>");
        foreach (var item in items)
        {
            result.Append($"<li>{item}</li>");
        }

        result = result.Append("</ul>");

        return new HtmlString(result.ToString());
    }
}