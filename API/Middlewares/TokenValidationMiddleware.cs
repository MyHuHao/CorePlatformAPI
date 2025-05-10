namespace API.Middlewares;

public class TokenValidationMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

        if (token != null)
            // 检查黑名单
            if (false)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token revoked");
                return;
            }

        await next(context);
    }
}