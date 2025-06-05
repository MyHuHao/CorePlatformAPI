using Application.Commands;
using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces;
using Core.Interfaces.Services;

namespace Application.Services;

public class RoleGroupService(
    IMapper mapper,
    RoleGroupQuery query,
    RoleGroupCommand command,
    IUnitOfWork unitOfWork
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
    ///     分页查询角色组
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

    public async Task<ApiResult<string>> RoleGroupAuthorizeAsync(RoleGroupAuthorizeRequest request)
    {
        try
        {
            await unitOfWork.BeginTransactionAsync();
            await ProcessResourcesAsync(request);
            await ProcessMenusAsync(request);
            await unitOfWork.CommitAsync();
            return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "授权成功" };
        }
        catch (Exception exception)
        {
            await unitOfWork.RollbackAsync();
            throw new BadRequestException(MsgCodeEnum.Error, exception.Message);
        }
    }

    // 通过ID获取已经授权的菜单和资源
    public async Task<ApiResult<List<string>>> GetRoleGroupAuthorizeByIdAsync(ByRoleGroupRequest request)
    {
        var menusTask = query.GetRoleGroupMenuByIdAsync(request.CompanyId, request.RoleGroupId);
        var resourcesTask = query.GetRoleGroupResByIdAsync(request.CompanyId, request.RoleGroupId);
        await Task.WhenAll(menusTask, resourcesTask);

        var menus = await menusTask;
        var resources = (await resourcesTask).ToList();

        var idSet = new HashSet<string>();

        foreach (var menu in menus)
        {
            idSet.Add(menu.WebMenuId);
        }

        // 添加资源ID和关联的WebMenuId
        // 如果res.WebMenuId 和 menu.WebMenuId 相等，就请删除掉menu.WebMenuId
        foreach (var res in resources)
        {
            idSet.Add(res.ResId);
            if (idSet.Contains(res.WebMenuId))
            {
                idSet.Remove(res.WebMenuId);
            }
        }

        return new ApiResult<List<string>>
        {
            Data = idSet.ToList(),
            MsgCode = MsgCodeEnum.Success,
            Msg = "获取成功"
        };
    }

    private async Task ProcessResourcesAsync(RoleGroupAuthorizeRequest request)
    {
        // 获取现有资源关联
        var existingResources = await query.GetAllRoleGroupResourceByIdAsync(request.CompanyId, request.RoleGroupId);

        var requestRoleGroups = new HashSet<string>(request.ResIds);
        var existingRoleGroups = new HashSet<string>(existingResources);
        
        var toDelete = existingRoleGroups.Except(requestRoleGroups).ToList();
        var toAdd = requestRoleGroups.Except(existingRoleGroups).ToList();
        
        // 执行删除操作
        if (toDelete.Count != 0)
        {
            var deleteQuery = new RoleGroupAuthorizeRequest
            {
                CompanyId = request.CompanyId,
                RoleGroupId = request.RoleGroupId,
                ResIds = toDelete,
                MenuIds = [],
                StaffId = request.StaffId
            };
            await command.DeleteRoleGroupResourceAsync(deleteQuery);
        }

        // 执行新增操作
        if (toAdd.Count != 0)
        {
            var addQuery = new RoleGroupAuthorizeRequest
            {
                CompanyId = request.CompanyId,
                RoleGroupId = request.RoleGroupId,
                ResIds = toAdd,
                MenuIds = [],
                StaffId = request.StaffId
            };
            await command.AddRoleGroupResourceAsync(addQuery);
        }
    }

    private async Task ProcessMenusAsync(RoleGroupAuthorizeRequest request)
    {
        // 获取现有菜单关联
        var existingMenus = await query.GetAllRoleGroupWebMenuByIdAsync(request.CompanyId, request.RoleGroupId);

        var requestRoleGroups = new HashSet<string>(request.MenuIds);
        var existingRoleGroups = new HashSet<string>(existingMenus);
        
        var toDelete = existingRoleGroups.Except(requestRoleGroups).ToList();
        var toAdd = requestRoleGroups.Except(existingRoleGroups).ToList();

        // 执行删除
        if (toDelete.Count != 0)
        {
            var deleteQuery = new RoleGroupAuthorizeRequest
            {
                CompanyId = request.CompanyId,
                RoleGroupId = request.RoleGroupId,
                ResIds = [],
                MenuIds = toDelete,
                StaffId = request.StaffId
            };
            await command.DeleteRoleGroupMenusAsync(deleteQuery);
        }

        // 执行新增
        if (toAdd.Count != 0)
        {
            var addQuery = new RoleGroupAuthorizeRequest
            {
                CompanyId = request.CompanyId,
                RoleGroupId = request.RoleGroupId,
                ResIds = [],
                MenuIds = toAdd,
                StaffId = request.StaffId
            };
            await command.AddRoleGroupMenusAsync(addQuery);
        }
    }
}