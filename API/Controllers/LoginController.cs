using Application.Interfaces;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class LoginController(ILoginService service) : Controller
{
    // 登录验证,获取Token
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var result = await service.Login(request);
            return Ok(result);
        }
        catch (Exception e)
        {
            var result = new ApiResults<string> { MsgCode = 2 ,Msg = e.Message};
            return BadRequest(result);
        }
    }
}