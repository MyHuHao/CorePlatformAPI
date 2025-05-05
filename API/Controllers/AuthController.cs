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

}