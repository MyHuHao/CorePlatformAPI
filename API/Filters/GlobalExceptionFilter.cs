using Core.Contracts.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var result = new ApiResults<string>
        {
            MsgCode = 1,
            Msg = context.Exception.Message
        };

        context.Result = new BadRequestObjectResult(result);
        context.ExceptionHandled = true;
    }
}