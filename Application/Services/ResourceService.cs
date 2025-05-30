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

public class ResourceService(ResourceQuery query, ResourceCommand command, IMapper mapper) : IResourceService
{
    public async Task<ApiResult<PagedResult<ResourceDto>>> GetResourceByPageAsync(ByResourceListRequest request)
    {
        var result = await query.GetResourceByPageAsync(request);
        var resultDto = mapper.Map<List<ResourceDto>>(result.items);
        PagedResult<ResourceDto> pagedResult = new()
        {
            Records = resultDto,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<ResourceDto>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }

    public async Task<ApiResult<string>> AddResourceAsync(AddResourceRequest request)
    {
        // 公司和菜单ID不能为空  
        if (string.IsNullOrEmpty(request.CompanyId))
            throw new ValidationException(MsgCodeEnum.Warning, "所属公司不能为空");

        if (string.IsNullOrEmpty(request.WebMenuId))
            throw new ValidationException(MsgCodeEnum.Warning, "所属菜单不能为空");

        // 验证是否重复
        var validResourceCode = await query.ValidResourceAsync(new ValidResourceCodeRequest
        {
            CompanyId = request.CompanyId,
            WebMenuId = request.WebMenuId,
            ResCode = request.ResCode
        });
        if (validResourceCode) throw new ValidationException(MsgCodeEnum.Warning, "资源编码已存在");
        
        // 验证是否重复
        var validResSequence = await query.ValidResourceAsync(new ValidResourceCodeRequest
        {
            CompanyId = request.CompanyId,
            WebMenuId = request.WebMenuId,
            ResSequence = request.ResSequence
        });
        if (validResSequence) throw new ValidationException(MsgCodeEnum.Warning, "排序已存在");

        // 新增数据
        await command.AddResourceAsync(request);

        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "创建成功" };
    }

    public async Task<ApiResult<string>> UpdateResourceAsync(UpdateResourceRequest request)
    {
        // 验证是否重复
        var validResourceCode = await query.ValidResourceAsync(new ValidResourceCodeRequest
        {
            ResourceId = request.Id,
            CompanyId = request.CompanyId,
            WebMenuId = request.WebMenuId,
            ResCode = request.ResCode
        });
        if (validResourceCode) throw new ValidationException(MsgCodeEnum.Warning, "资源编码已存在");

        // 验证排序是否重复
        var validResourceSequence = await query.ValidResourceAsync(new ValidResourceCodeRequest
        {
            ResourceId = request.Id,
            CompanyId = request.CompanyId,
            WebMenuId = request.WebMenuId,
            ResSequence = request.ResSequence
        });
        if (validResourceSequence) throw new ValidationException(MsgCodeEnum.Warning, "排序已存在");

        await command.UpdateResourceAsync(request);

        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "修改成功" };
    }

    public async Task<ApiResult<string>> DeleteResourceByIdAsync(string id)
    {
        await command.DeleteResourceByIdAsync(id);
        return new ApiResult<string> { MsgCode = MsgCodeEnum.Success, Msg = "删除成功" };
    }

    public async Task<ApiResult<ResourceDto>> GetResourceByIdAsync(string id)
    {
        var resource = await query.GetResourceByIdAsync(id);
        if (resource == null) throw new NotFoundException(MsgCodeEnum.Warning, "资源不存在");
        return new ApiResult<ResourceDto>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = mapper.Map<ResourceDto>(resource) };
    }
}