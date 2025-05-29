using Core.Contracts.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class AccountController(IAccountService service) : ControllerBase
{
    // 账号分页查询
    [HttpPost]
    public async Task<IActionResult> GetAccountByPage([FromBody] ByAccountListRequest request)
    {
        var result = await service.GetAccountPageAsync(request);
        return Ok(result);
    }

    // 新增
    [HttpPost]
    public async Task<IActionResult> AddAccount([FromBody] AddAccountRequest request)
    {
        var result = await service.AddAccountAsync(request);
        return Ok(result);
    }

    // 删除
    [HttpPost]
    public async Task<IActionResult> DeleteAccountById([FromBody] string id)
    {
        var result = await service.DeleteAccountAsync(id);
        return Ok(result);
    }

    // 通过ID查询详细
    [HttpPost]
    public async Task<IActionResult> GetAccountById([FromBody] string id)
    {
        var result = await service.GetAccountByIdAsync(id);
        return Ok(result);
    }
}