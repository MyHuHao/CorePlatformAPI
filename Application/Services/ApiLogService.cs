using Application.Commands;
using Application.Queries;
using AutoMapper;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;

namespace Application.Services;

public class ApiLogService(IMapper mapper, ApiLogCommand command, ApiLogQuery query) : IApiLogService
{
    /// <summary>
    ///     插入api日志
    /// </summary>
    /// <param name="apiLog"></param>
    public async Task InsertApiLog(ApiLog apiLog)
    {
        await command.AddAsync(apiLog);
    }

    public async Task<ApiResult<PagedResult<ApiLogDto>>> GetApiLogByPage(ApiLogRequest request)
    {
        var result = await query.GetApiLogByPage(request);
        var apiLogDto =  mapper.Map<List<ApiLogDto>>(result.items);
        PagedResult<ApiLogDto> pagedResult = new()
        {
            Records = apiLogDto,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<ApiLogDto>> { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }
}