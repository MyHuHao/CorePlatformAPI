using System.Net;
using System.Text.Json;
using Core.Contracts.Results;
using Core.Enums;

namespace API.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError("api未知接口错误: {Message}", ex.Message);
            await HandleSystemExceptionAsync(context, ex);
        }
    }

    private static Task HandleSystemExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var result = new ApiResult<object>
        {
            MsgCode = MsgCodeEnum.Error,
            Msg = exception.Message
        };

        var json = JsonSerializer.Serialize(result);
        return context.Response.WriteAsync(json);
    }
}