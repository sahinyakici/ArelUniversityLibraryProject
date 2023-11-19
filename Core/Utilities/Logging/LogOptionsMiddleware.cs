using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace Core.Utilities.Logging;

public class LogOptionsMiddleware
{
    private readonly RequestDelegate next;

    public LogOptionsMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public Task Invoke(HttpContext context)
    {
        string userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        string userMail = context.User.FindFirst(ClaimTypes.Email)?.Value;
        LogContext.PushProperty("UserId", userId);
        LogContext.PushProperty("UserMail", userMail);

        return next(context);
    }
}