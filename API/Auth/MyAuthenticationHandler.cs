using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace API.Auth;

public class MyAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock,
    IDistributedCache cache)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // 从 Authorization 请求头获取 Token
        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.NoResult();

        string token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (string.IsNullOrEmpty(token))
            return AuthenticateResult.Fail("缺少Token");

        // 验证 Redis 中是否存在该 Token
        var userData = await cache.GetStringAsync(token);
        if (userData == null)
            return AuthenticateResult.Fail("无效或已过期Token");

        // 令牌有效，构造用户身份
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userData) // 假设 userData 为用户名
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return AuthenticateResult.Success(ticket);
    }
    
}