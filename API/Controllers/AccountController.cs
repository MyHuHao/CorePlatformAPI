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
    [HttpDelete]
    public async Task<IActionResult> DeleteAccountById(string id, string companyId)
    {
        var result = await service.DeleteAccountAsync(id, companyId);
        return Ok(result);
    }

    // 通过ID查询详细
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAccountById(string id)
    {
        var result = await service.GetAccountByIdAsync(id);
        return Ok(result);
    }

    // 修改账号数据
    [HttpPost]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
    {
        var result = await service.UpdateAccountAsync(request);
        return Ok(result);
    }

    // 验证原密码是否正确
    [HttpPost]
    public async Task<IActionResult> VerifyPassword([FromBody] VerifyPasswordRequest request)
    {
        var result = await service.VerifyPasswordAsync(request);
        return Ok(result);
    }

    // 修改密码
    [HttpPost]
    public async Task<IActionResult> UpdatePassword([FromBody] VerifyPasswordRequest request)
    {
        var result = await service.UpdatePasswordAsync(request);
        return Ok(result);
    }

    // 给菜单授权角色组
    [HttpPost]
    public async Task<IActionResult> GrantMenuRole([FromBody] GrantMenuRoleRequest request)
    {
        var result = await service.GrantMenuRoleAsync(request);
        return Ok(result);
    }

    // 通过ID查询已经授权的角色组
    [HttpGet]
    public async Task<IActionResult> GetGrantMenuRoleById(string companyId, string id)
    {
        var result = await service.GetGrantMenuRoleByIdAsync(companyId, id);
        return Ok(result);
    }
}