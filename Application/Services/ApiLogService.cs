using Application.Commands;
using Application.Queries;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Entities;
using Core.Enums;
using Core.Interfaces.Services;

namespace Application.Services;

public class ApiLogService(ApiLogCommand command, ApiLogQuery query) : IApiLogService
{
    /// <summary>
    ///     插入api日志
    /// </summary>
    /// <param name="apiLog"></param>
    public async Task InsertApiLog(ApiLog apiLog)
    {
        await command.AddAsync(apiLog);
    }

    public async Task<ApiResult<PagedResult<ApiLog>>> GetApiLogByPage(ApiLogRequest request)
    {
        var result = await query.GetApiLogByPage(request);
        PagedResult<ApiLog> pagedResult = new()
        {
            Records = result.items,
            Page = request.Page,
            PageSize = request.PageSize,
            Total = result.total
        };
        return new ApiResult<PagedResult<ApiLog>> { MsgCode = MsgCodeEnum.Success, Msg = "查询成功", Data = pagedResult };
    }
}