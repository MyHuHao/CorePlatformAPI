using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Services;

namespace Application.Services;

public class WebMenuService(IMapper mapper, WebMenuQuery query) : IWebMenuService
{
    // 菜单分页查询
    public async Task<ApiResult<PagedResult<WebMenuListResult>>> GetWebMenuByPageAsync(ByWebMenuListRequest request)
    {
        var result = await query.GetWebMenuPageAsync(request);
        var webMenuDto = mapper.Map<List<WebMenuDto>>(result.items);
        PagedResult<WebMenuListResult> pagedResult = new()
        {
            Records = FormatWebMenuListResult(webMenuDto),
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<WebMenuListResult>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }

    // 新增菜单
    public async Task<ApiResult<string>> AddWebMenuAsync(AddWebMenuRequest request)
    {
        // 延时1s
        await Task.Delay(1000);
        return new ApiResult<string>() { MsgCode = MsgCodeEnum.Success, Msg = "创建成功" };
    }


    // 格式化 webMenuDto 为 树形数组
    private static List<WebMenuListResult> FormatWebMenuListResult(List<WebMenuDto> webMenuDto)
    {
        return BuildTree(string.Empty);

        List<WebMenuListResult> BuildTree(string parentId)
        {
            return webMenuDto
                .Where(dto => dto.ParentWebMenuId == parentId)
                .Select(dto => new WebMenuListResult
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
                    Children = BuildTree(dto.WebMenuId)
                }).OrderBy(m => m.Sequence).ToList();
        }
    }
}