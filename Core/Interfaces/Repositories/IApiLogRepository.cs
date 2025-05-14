using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IApiLogRepository
{
    /// <summary>
    ///     插入数据
    /// </summary>
    /// <param name="apiLog"></param>
    /// <returns></returns>
    Task<bool> AddAsync(ApiLog apiLog);
    
    Task<(IEnumerable<ApiLog> items, int total)> GetApiLogByPage(ApiLogRequest request);
}