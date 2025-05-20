using Core.Contracts;
using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces.Services;

public interface IApiLogService
{
    /// <summary>
    ///     插入api日志
    /// </summary>
    /// <param name="apiLog"></param>
    /// <returns></returns>
    Task InsertApiLog(ApiLog apiLog);

    Task<ApiResult<PagedResult<ApiLogDto>>> GetApiLogByPage(ApiLogRequest request);
}