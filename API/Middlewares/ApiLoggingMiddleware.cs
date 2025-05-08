using System.Text;
using System.Text.Json;
using Application.Interfaces;
using Core.Entities;

namespace API.Middlewares;

public class ApiLoggingMiddleware(
    RequestDelegate next,
    ILogger<ApiLoggingMiddleware> logger,
    IServiceScopeFactory scopeFactory)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;
        var originalResponseBody = context.Response.Body;
        var requestBody = await ReadRequestBodyAsync(context.Request);

        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        var errorMessage = "";
        var statusCode = 200;

        try
        {
            await next(context);
            statusCode = context.Response.StatusCode;
        }
        catch (Exception ex)
        {
            statusCode = 500;
            errorMessage = ex.Message;
            await HandleExceptionAsync(context, ex);
        }
        finally
        {
            // 手动创建作用域
            using var scope = scopeFactory.CreateScope();
            // 从当前作用域解析服务
            var apiLogService = scope.ServiceProvider.GetRequiredService<IApiLogService>();
            var responseBody = await ReadResponseBodyAsync(responseBodyStream, originalResponseBody);
            var duration = (int)(DateTime.UtcNow - startTime).TotalMilliseconds;
            await LogApiRequest(apiLogService, context, startTime, duration, requestBody, responseBody, statusCode,
                errorMessage);
            await responseBodyStream.CopyToAsync(originalResponseBody);
        }
    }

    private static async Task<string> ReadRequestBodyAsync(HttpRequest request)
    {
        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 4096, true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0;
        return Truncate(body, 4096);
    }

    private static async Task<string> ReadResponseBodyAsync(Stream responseBody, Stream originalBody)
    {
        responseBody.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(responseBody, Encoding.UTF8, true, 4096, true);
        var body = await reader.ReadToEndAsync();
        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBody);
        return Truncate(body, 4096);
    }

    private async Task LogApiRequest(
        IApiLogService apiLogService,
        HttpContext context,
        DateTime requestTime,
        int duration,
        string requestBody,
        string responseBody,
        int statusCode,
        string errorMessage
    )
    {
        var logData = new ApiLog
        {
            Id = "",
            IpAddress = context.Connection.RemoteIpAddress?.ToString() ?? "",
            UserName = context.User.Identity is { IsAuthenticated: true, Name: not null }
                ? context.User.Identity.Name
                : "匿名",
            Path = context.Request.Path,
            Method = context.Request.Method,
            RequestBody = requestBody,
            ResponseBody = responseBody,
            StatusCode = (short)statusCode,
            ErrorMessage = errorMessage,
            RequestTime = requestTime,
            Duration = duration
        };

        // 使用 apiLogService 处理日志逻辑
        await apiLogService.InsertApiLog(logData);

        LogToConsole(logData);
    }

    private void LogToConsole(ApiLog log)
    {
        var logLevel = log.StatusCode >= 500 ? LogLevel.Error : LogLevel.Information;
        logger.Log(logLevel,
            "API {Method} {Path} responded {StatusCode} in {Duration}ms",
            log.Method, log.Path, log.StatusCode, log.Duration);
    }

    private static string Truncate(string value, int maxLength) =>
        value.Length <= maxLength ? value : value[..maxLength];

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        return context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            error = "An internal server error occurred",
            detail = exception.Message
        }));
    }
}