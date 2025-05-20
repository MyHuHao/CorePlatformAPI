using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;

namespace Core.Interfaces.Services;

public interface IApiLogService
{
    /// <summary>
    ///     插入api日志
    /// </summary>
    /// <param name="apiLog"></param>
    /// <returns></returns>
    Task AddApiLogAsync(AddApiLogRequest apiLog);

    /// <summary>
    ///     分页查询-获取访问日志
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ApiResult<PagedResult<ApiLogDto>>> GetByApiLogPage(ByApiLogListRequest request);
}