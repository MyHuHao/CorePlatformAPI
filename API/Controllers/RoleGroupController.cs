using Core.Contracts.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class RoleGroupController(IRoleGroupService service) : Controller
{
    /// <summary>
    ///     新增角色组
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddRoleGroup([FromBody] AddRoleGroupRequest request)
    {
        var result = await service.AddRoleGroupAsync(request);
        return Ok(result);
    }

    /// <summary>
    ///     分页查询角色组
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> GetRoleGroupPage([FromBody] ByRoleGroupListRequest request)
    {
        var result = await service.GetRoleGroupPageAsync(request);
        return Ok(result);
    }

    /// <summary>
    ///     删除角色组
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await service.DeleteRoleGroupAsync(id);
        return Ok(result);
    }

    /// <summary>
    ///     通过id查询角色组详细
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoleGroupById(string id)
    {
        var result = await service.GetRoleGroupByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    ///     修改角色组
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> UpdateRoleGroup([FromBody] UpdateRoleGroupRequest request)
    {
        var result = await service.UpdateRoleGroupAsync(request);
        return Ok(result);
    }
}