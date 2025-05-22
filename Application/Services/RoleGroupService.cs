using Application.Commands;
using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;

namespace Application.Services;

public class RoleGroupService(
    IMapper mapper,
    RoleGroupQuery query,
    RoleGroupCommand command
) : IRoleGroupService
{
    public async Task<ApiResult<string>> AddRoleGroupAsync(AddRoleGroupRequest request)
    {
        // 验证是否重复
        var validRole = await query.ValidRoleGroup(new ValidRoleGroupRequest
        {
            CompanyId = request.CompanyId,
            RoleGroupName = request.RoleGroupName
        });
        if (validRole) throw new ValidationException(MsgCodeEnum.Warning, "角色组已存在");

        // 开始新增
        await command.AddRoleGroupAsync(request);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "创建成功" };
    }

    /// <summary>
    /// 分页查询角色组
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResult<PagedResult<RoleGroupDto>>> GetRoleGroupPageAsync(ByRoleGroupListRequest request)
    {
        var result = await query.GetRoleGroupPageAsync(request);
        var roleGroupDto = mapper.Map<List<RoleGroupDto>>(result.items);
        PagedResult<RoleGroupDto> pagedResult = new()
        {
            Records = roleGroupDto,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<RoleGroupDto>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }

    public async Task<ApiResult<RoleGroupDto>> GetRoleGroupByIdAsync(string id)
    {
        var result = await query.GetRoleGroupByIdAsync(id);
        if (result == null) throw new NotFoundException(MsgCodeEnum.Warning, "角色组不存在");
        return new ApiResult<RoleGroupDto>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = mapper.Map<RoleGroupDto>(result) };
    }

    public async Task<ApiResult<string>> DeleteRoleGroupAsync(string id)
    {
        await command.DeleteRoleGroupAsync(id);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "删除成功" };
    }

    //修改角色组
    public async Task<ApiResult<string>> UpdateRoleGroupAsync(UpdateRoleGroupRequest request)
    {
        // 验证是否存在
        var validRole = await query.ValidRoleGroup(new ValidRoleGroupRequest
        {
            CompanyId = request.CompanyId,
            RoleGroupId = request.RoleGroupId
        });
        if (!validRole) throw new ValidationException(MsgCodeEnum.Warning, "角色组不存在");

        // 开始修改
        await command.UpdateRoleGroupAsync(request);

        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "修改成功" };
    }
}