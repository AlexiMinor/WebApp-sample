using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.MVC7.Filters;

public class LastVisitTrackerResourceFilter : Attribute, IResourceFilter
{
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        context.HttpContext.Response.Cookies.Append("LastVisitTime", DateTime.Now.ToString("R"));
        context.Result = new ContentResult() { Content = "12345" };
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        

    }
}
//Tue 014 Nov 02023 017%3A54%3A03 0GMT