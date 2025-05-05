using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class OperationLogAttribute(IOperationLogRepository logRepo) : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executedContext = await next();
        if (executedContext.Exception == null)
        {
            if (context.ActionDescriptor.DisplayName != null && context.HttpContext.User.Identity != null &&
                context.HttpContext.Connection.RemoteIpAddress != null)
            {
                var log = new OperationLog
                {
                    Username = context.HttpContext.User.Identity.Name ?? "匿名",
                    Controller = context.Controller.GetType().Name,
                    Action = context.ActionDescriptor.DisplayName,
                    IpAddress = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Timestamp = DateTime.Now
                };
                await logRepo.InsertAsync(log);
            }
        }
    }
}