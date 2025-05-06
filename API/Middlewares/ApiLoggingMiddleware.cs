using System.Text;
using System.Text.Json;

namespace API.Middlewares;

public class ApiLoggingMiddleware(RequestDelegate next, ILogger<ApiLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;
        var requestBody = await ReadRequestBodyAsync(context.Request);
        var originalResponseBody = context.Response.Body;

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
            var responseBody = await ReadResponseBodyAsync(responseBodyStream, originalResponseBody);
            var duration = (long)(DateTime.UtcNow - startTime).TotalMilliseconds;

            LogApiRequest(context, startTime, duration, requestBody, responseBody, statusCode, errorMessage);
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

    private void LogApiRequest(
        HttpContext context,
        DateTime requestTime,
        long duration,
        string requestBody,
        string responseBody,
        int statusCode,
        string errorMessage)
    {
        var logData = new
        {
            IpAddress = context.Connection.RemoteIpAddress?.ToString(),
            UserName = context.User.Identity?.IsAuthenticated == true ? context.User.Identity.Name : null,
            context.Request.Path,
            context.Request.Method,
            RequestBody = requestBody,
            ResponseBody = responseBody,
            StatusCode = statusCode,
            ErrorMessage = errorMessage,
            RequestTime = requestTime,
            Duration = duration
        };

        if (statusCode >= 500)
        {
            logger.LogError("API Error: {@ApiLog}", logData);
        }
        else
        {
            logger.LogInformation("API Request: {@ApiLog}", logData);
        }
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