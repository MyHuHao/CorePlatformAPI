using Application.Interfaces;
using Core.Contracts.Requests;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LoginController(ILoginService service) : Controller
{
    /// <summary>
    /// 创建账号
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
    {
        var result = await service.CreateAccount(request);
        return Ok(result);
    }
    
    // 登录验证,获取Token
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await service.Login(request);
        return Ok(result);
    }
}