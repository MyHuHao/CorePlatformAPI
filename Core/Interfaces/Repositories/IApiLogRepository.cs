using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IApiLogRepository
{
    /// <summary>
    /// 通过Id查询api接口日志
    /// </summary>
    /// <returns></returns>
    Task<ApiLog?> GetByApiLogAsync(string id);

    /// <summary>
    /// 查询api接口日志列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<(IEnumerable<ApiLog> items, int total)> GetByAccountListAsync(ByApiLogListRequest request);

    /// <summary>
    /// 新增api接口日志
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<int> AddAccountAsync(AddApiLogRequest request);
}