using System.Text;

namespace API.Middlewares;

/// <summary>
/// 全局请求响应日志中间件，记录请求和响应信息
/// </summary>
public class GlobalLoggingMiddleware(RequestDelegate next, ILogger<GlobalLoggingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        // 记录请求
        await LogRequest(context);

        // 使用内存流捕获响应
        var originalBody = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        try
        {
            await next(context); // 调用后续中间件
            // 记录响应
            await LogResponse(context);
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBody);
        }
        catch (Exception ex)
        {
            // 记录异常
            logger.LogError(ex, "处理请求时发生异常");
            throw; // 可统一封装返回错误信息
        }
    }

    private async Task LogRequest(HttpContext context)
    {
        context.Request.EnableBuffering();
        var request = context.Request;
        var body = string.Empty;

        if (request is { ContentLength: > 0, Body.CanRead: true })
        {
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadExactlyAsync(buffer, 0, buffer.Length);
            body = Encoding.UTF8.GetString(buffer);
            request.Body.Seek(0, SeekOrigin.Begin);
        }

        logger.LogInformation("Incoming Request: {method} {url} Headers: {headers} Body: {body}",
            request.Method, request.Path, request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), body);
    }

    private async Task LogResponse(HttpContext context)
    {
        var response = context.Response;
        response.Body.Seek(0, SeekOrigin.Begin);
        string text = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        logger.LogInformation("Outgoing Response: {statusCode} Body: {body}", response.StatusCode, text);
    }
}