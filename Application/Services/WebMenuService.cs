using Application.Commands;
using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Services;

namespace Application.Services;

public class WebMenuService(IMapper mapper, WebMenuQuery query, WebMenuCommand command, ResourceQuery resourceQuery)
    : IWebMenuService
{
    // 菜单分页查询
    public async Task<ApiResult<PagedResult<WebMenuResult>>> GetWebMenuByPageAsync(ByWebMenuListRequest request)
    {
        var result = await query.GetWebMenuPageAsync(request);
        var webMenuDto = mapper.Map<List<WebMenuDto>>(result.items);
        PagedResult<WebMenuResult> pagedResult = new()
        {
            Records = FormatWebMenuListResult(webMenuDto),
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<WebMenuResult>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }

    // 获取所有父级菜单
    public async Task<ApiResult<List<ParentWebMenuListResult>>> GetParentWebMenuListAsync()
    {
        // 获取所有的数据
        var result = await query.GetWebMenuPageAsync(new ByWebMenuListRequest
        {
            Name = "",
            Page = 1,
            PageSize = 2000
        });
        var webMenuDto = mapper.Map<List<WebMenuDto>>(result.items);

        // 格式化数据格式
        return new ApiResult<List<ParentWebMenuListResult>>
        {
            MsgCode = MsgCodeEnum.Success,
            Msg = "查询成功",
            Data = FormatParentWebMenuListResult(webMenuDto)
        };
    }

    // 新增菜单
    public async Task<ApiResult<string>> AddWebMenuAsync(AddWebMenuRequest request)
    {
        // 新增的时候验证 在当前父节点下 序号是否已存在
        var verifySequence = await query.VerifyWebMenuAsync(new VerifyWebMenuRequest
        {
            ParentWebMenuId = request.ParentWebMenuId,
            Sequence = request.Sequence
        });
        if (verifySequence) throw new ValidationException(MsgCodeEnum.Warning, "当前父级菜单下显示顺序已存在");

        // 验证名称是否已经在菜单中存在
        var verifyName = await query.VerifyWebMenuAsync(new VerifyWebMenuRequest
        {
            Name = request.Name
        });
        if (verifyName) throw new ValidationException(MsgCodeEnum.Warning, "菜单名称已存在");

        await command.AddWebMenuAsync(request);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "创建成功" };
    }

    // 修改菜单
    public async Task<ApiResult<string>> UpdateWebMenuAsync(UpdateWebMenuRequest request)
    {
        // 新增的时候验证 在当前父节点下 序号是否已存在
        var verifySequence = await query.VerifyWebMenuAsync(new VerifyWebMenuRequest
        {
            ParentWebMenuId = request.ParentWebMenuId,
            Sequence = request.Sequence
        });
        if (verifySequence == false) throw new ValidationException(MsgCodeEnum.Warning, "当前父级菜单下序号已存在");

        // 验证名称是否已经在菜单中存在
        var verifyName = await query.VerifyWebMenuAsync(new VerifyWebMenuRequest
        {
            Name = request.Name
        });
        if (verifyName == false) throw new ValidationException(MsgCodeEnum.Warning, "名称已存在");

        await command.UpdateWebMenuAsync(request);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "修改成功" };
    }

    // 删除菜单
    public async Task<ApiResult<string>> DeleteWebMenuByIdAsync(string id, string companyId, string webMenuId)
    {
        var hasChild = await query.VerifyWebMenuHasChildAsync(webMenuId);
        if (hasChild) throw new ValidationException(MsgCodeEnum.Warning, "该菜单下有子菜单，不能删除");

        var hasRoleGroup = await query.VerifyWebMenuHasRoleGroupAsync(companyId, webMenuId);
        if (hasRoleGroup) throw new ValidationException(MsgCodeEnum.Warning, "该菜单有绑定角色组，不能删除");

        await command.DeleteWebMenuByIdAsync(id);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "删除成功" };
    }

    public async Task<ApiResult<List<WebMenuResourceListResult>>> GetWebMenuResourceListAsync(
        ByWebMenuResourceRequest request)
    {
        // 获取所有的数据
        var result = await query.GetWebMenuPageAsync(new ByWebMenuListRequest
        {
            Name = "",
            Page = 1,
            PageSize = 2000
        });
        var webMenuDto = mapper.Map<List<WebMenuDto>>(result.items);

        // 格式化数据格式
        var list = await FormatWebMenuResourceListResult(request.CompanyId, webMenuDto);

        return new ApiResult<List<WebMenuResourceListResult>>
        {
            MsgCode = MsgCodeEnum.Success,
            Msg = "查询成功",
            Data = list
        };
    }

    private async Task<List<WebMenuResourceListResult>> GetResourceListAsync(string companyId, string webMenuId)
    {
        return await resourceQuery.GetResourceListAsync(companyId, webMenuId);
    }

    // 格式化 webMenuDto 为 树形数组
    private async Task<List<WebMenuResourceListResult>> FormatWebMenuResourceListResult(string companyId,
        List<WebMenuDto> webMenuDto)
    {
        var lookup = webMenuDto.ToLookup(d => d.ParentWebMenuId);

        return await BuildTree(string.Empty);

        async Task<List<WebMenuResourceListResult>> BuildTree(string parentId)
        {
            // 获取并排序当前层级的节点
            var currentLevel = lookup[parentId]
                .OrderBy(dto => Convert.ToInt32(dto.Sequence))
                .ToList();

            // 准备所有节点的任务
            var nodeTasks = currentLevel.Select(async dto =>
            {
                // 并发获取资源列表
                var resourceList = await GetResourceListAsync(companyId, dto.WebMenuId);
                var isPenultimate = resourceList.Count > 0;

                // 递归获取子节点（如果没有资源）
                var children = isPenultimate
                    ? resourceList
                    : await BuildTree(dto.WebMenuId);

                return new WebMenuResourceListResult
                {
                    Id = dto.WebMenuId,
                    Label = dto.Title,
                    Sequence = dto.Sequence,
                    Type = "menu",
                    WebMenuId = dto.WebMenuId,
                    IsPenultimate = isPenultimate,
                    Children = children
                };
            }).ToList();

            // 并发执行所有任务
            return (await Task.WhenAll(nodeTasks)).ToList();
        }
    }

    // 格式化 webMenuDto 为 树形数组
    private static List<ParentWebMenuListResult> FormatParentWebMenuListResult(List<WebMenuDto> webMenuDto)
    {
        var lookup = webMenuDto.ToLookup(d => d.ParentWebMenuId);
        return BuildTree(string.Empty).ToList();

        IEnumerable<ParentWebMenuListResult> BuildTree(string parentId)
        {
            var sortedItems = lookup[parentId]
                .OrderBy(dto => Convert.ToInt32(dto.Sequence))
                .ToList();
            foreach (var dto in sortedItems)
            {
                yield return new ParentWebMenuListResult
                {
                    Value = dto.WebMenuId,
                    ParentWebMenuId = dto.ParentWebMenuId,
                    Sequence = dto.Sequence,
                    Label = dto.Title,
                    Children = BuildTree(dto.WebMenuId).ToList()
                };
            }
        }
    }

    // 格式化 webMenuDto 为 树形数组
    private static List<WebMenuResult> FormatWebMenuListResult(List<WebMenuDto> webMenuDto)
    {
        var lookup = webMenuDto.ToLookup(d => d.ParentWebMenuId);
        return BuildTree(string.Empty).ToList();

        IEnumerable<WebMenuResult> BuildTree(string parentId)
        {
            var sortedItems = lookup[parentId]
                .OrderBy(dto => Convert.ToInt32(dto.Sequence))
                .ToList();

            foreach (var dto in sortedItems)
            {
                yield return new WebMenuResult
                {
                    Id = dto.Id,
                    WebMenuId = dto.WebMenuId,
                    ParentWebMenuId = dto.ParentWebMenuId,
                    Name = dto.Name,
                    Path = dto.Path,
                    Component = dto.Component,
                    Title = dto.Title,
                    Icon = dto.Icon,
                    Redirect = dto.Redirect,
                    Sequence = dto.Sequence,
                    IsFrame = dto.IsFrame,
                    FrameSrc = dto.FrameSrc,
                    IsCache = dto.IsCache,
                    IsVisible = dto.IsVisible,
                    Permission = dto.Permission,
                    MenuType = dto.MenuType,
                    Status = dto.Status,
                    Remark = dto.Remark,
                    Children = BuildTree(dto.WebMenuId).ToList()
                };
            }
        }
    }
}