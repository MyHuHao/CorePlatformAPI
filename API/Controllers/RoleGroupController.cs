﻿using Core.Contracts.Requests;
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
    /// <param name="companyId"></param>
    /// <param name="roleGroupId"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<IActionResult> Delete(string id, string companyId, string roleGroupId)
    {
        var result = await service.DeleteRoleGroupAsync(id, companyId, roleGroupId);
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

    // 角色组授权菜单和资源
    [HttpPost]
    public async Task<IActionResult> RoleGroupAuthorize([FromBody] RoleGroupAuthorizeRequest request)
    {
        var result = await service.RoleGroupAuthorizeAsync(request);
        return Ok(result);
    }

    // 通过ID获取已经授权的菜单和资源
    [HttpPost]
    public async Task<IActionResult> GetRoleGroupAuthorizeById([FromBody] ByRoleGroupRequest request)
    {
        var result = await service.GetRoleGroupAuthorizeByIdAsync(request);
        return Ok(result);
    }
}