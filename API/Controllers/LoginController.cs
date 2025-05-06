using API.Models;
using Application.Services;
using Core.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]

public class LoginController(LoginService service) : Controller
{
    // 登录验证,获取Token
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await service.Login(request);
        return Ok(result);
    }
}