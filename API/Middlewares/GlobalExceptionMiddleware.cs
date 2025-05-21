using System.Net;
using System.Text.Json;
using Core.Contracts;
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

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private static async Task HandleSystemExceptionAsync(HttpContext context, Exception exception)
    {
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
        }

        var result = new ApiResult<object>
        {
            MsgCode = MsgCodeEnum.Error,
            Msg = exception.Message
        };

        await context.Response.WriteAsJsonAsync(result, JsonOptions);
    }
}