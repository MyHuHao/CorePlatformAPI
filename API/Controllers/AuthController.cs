using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace API.Controllers;

public class AuthController(IDistributedCache cache) : Controller
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginLog dto)
    {
        // 验证用户名密码 (略)，假设验证通过
        var token = Guid.NewGuid().ToString("N"); // 32 位 UUID
        // 存储Token到Redis，设置过期，如1小时
        await cache.SetStringAsync(token, dto.Username, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        });
        return Ok(new { code = 0, token });
    }

    [Authorize(AuthenticationSchemes = "MyScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        // 假设当前用户已通过认证，生成新Token
        var oldToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var username = User.Identity?.Name ?? "";
        var newToken = Guid.NewGuid().ToString("N");
        // 删除旧 Token，存新 Token
        await cache.RemoveAsync(oldToken);
        await cache.SetStringAsync(newToken, username, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
        });
        return Ok(new { code = 0, token = newToken });
    }

    [Authorize(AuthenticationSchemes = "MyScheme")]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        await cache.RemoveAsync(token); // 移除Token实现下线
        return Ok(new { code = 0, msg = "已注销" });
    }
}