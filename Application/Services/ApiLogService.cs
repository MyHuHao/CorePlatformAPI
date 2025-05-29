using Application.Commands;
using Application.Queries;
using AutoMapper;
using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;
using Core.Enums;
using Core.Interfaces.Services;

namespace Application.Services;

public class ApiLogService(IMapper mapper, ApiLogCommand command, ApiLogQuery query) : IApiLogService
{
    /// <summary>
    ///     插入api日志
    /// </summary>
    /// <param name="apiLog"></param>
    public async Task AddApiLogAsync(AddApiLogRequest apiLog)
    {
        await command.AddAsync(apiLog);
    }

    /// <summary>
    ///     分页查询-获取访问日志
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<ApiResult<PagedResult<ApiLogDto>>> GetByApiLogPage(ByApiLogListRequest request)
    {
        var result = await query.ByApiLogListRequest(request);
        var apiLogDto = mapper.Map<List<ApiLogDto>>(result.items);
        PagedResult<ApiLogDto> pagedResult = new()
        {
            Records = apiLogDto,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<ApiLogDto>>
            { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }
}