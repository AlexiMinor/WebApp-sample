using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.MVC7.Filters;

public class TestResourceFilter : Attribute, IResourceFilter
{
    private int x;
    private string Token;

    public TestResourceFilter(int x, string token)
    {
        this.x = x;
        Token = token;
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        context.HttpContext.Response.Headers.Add("x", x.ToString());
        context.HttpContext.Response.Headers.Add("Token", Token);
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        throw new NotImplementedException();
    }
}