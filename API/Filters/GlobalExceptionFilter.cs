using Core.Contracts.Results;
using Core.Enums;
using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var result = new ApiResults<object>();

        if (context.Exception is BaseApiException baseEx)
            result.MsgCode = (int)baseEx.MsgCode;
        else
            result.MsgCode = (int)MsgCodeEnum.Error;

        result.Msg = context.Exception.Message;
        context.Result = new BadRequestObjectResult(result);
        context.ExceptionHandled = true;
    }
}