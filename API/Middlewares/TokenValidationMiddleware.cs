using System.Text.Json;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace API.Middlewares;

public class TokenValidationMiddleware(
    RequestDelegate next,
    ILogger<TokenValidationMiddleware> logger,
    IServiceScopeFactory scopeFactory)
{
    public async Task InvokeAsync(HttpContext context)
    {
        // 跳过公开端点（如登录、健康检查）
        if (IsAnonymousAllowed(context))
        {
            await next(context);
            return;
        }

        // 获取并提取Token
        var authHeader = context.Request.Headers.Authorization;
        if (StringValues.IsNullOrEmpty(authHeader))
        {
            await WriteErrorResponse(context, 401, "Token缺失");
            logger.LogWarning("请求缺少Authorization头，路径：{Path}", context.Request.Path.Value ?? "");
            return;
        }

        var token = authHeader.ToString().Split(' ').LastOrDefault() ?? "";
        if (string.IsNullOrEmpty(token))
        {
            await WriteErrorResponse(context, 401, "Token格式错误");
            logger.LogWarning("Authorization头格式不正确：{Header}", authHeader.ToString());
            return;
        }

        // 验证Token有效性
        try
        {
            using var scope = scopeFactory.CreateScope();
            var loginService = scope.ServiceProvider.GetRequiredService<ILoginService>();
            var isValid = await loginService.VerifyToken(token);

            if (!isValid)
            {
                await WriteErrorResponse(context, 403, "Token无效或已过期");
                logger.LogInformation("Token验证失败：{Token}", token);
                return;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Token验证过程中发生异常");
            await WriteErrorResponse(context, 500, "服务器内部错误");
            return;
        }

        await next(context);
    }


    private static async Task WriteErrorResponse(HttpContext context, int statusCode, string message)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";
        var response = JsonSerializer.Serialize(new { error = message });
        await context.Response.WriteAsync(response);
    }

    private static bool IsAnonymousAllowed(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        return endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null;
    }
}