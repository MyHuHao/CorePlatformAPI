using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController(IUserService service) : Controller
{
    /// <summary>
    ///     通过用户ID获取详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var result = await service.GetUserByIdAsync(id);
        return Ok(result);
    }
}