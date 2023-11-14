using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApp.Repositories;

namespace WebApp.MVC7.Filters;

public class UserAgentCheckerFilter : Attribute, IAsyncResourceFilter
{
    private readonly IUnitOfWork _unitOfWork;

    public UserAgentCheckerFilter(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {

        var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
        if (Regex.IsMatch(userAgent, "Googlebot/2.1"))
        {
            context.Result = new ContentResult() { Content = "No google bots allowed" };
        }
        else
        {
            await next();
        }
    }
}