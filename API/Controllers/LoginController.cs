using Core.Contracts.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class LoginController(ILoginService service) : Controller
{
    /// <summary>
    ///     创建账号
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
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await service.Login(request);
        return Ok(result);
    }

    // 获取登录类型
    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetLoginOptions()
    {
        var result = service.GetLoginOptions();
        return Ok(result);
    }
}