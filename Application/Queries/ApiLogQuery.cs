using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class ApiLogQuery(IApiLogRepository repository)
{
    /// <summary>
    ///     分页查询-获取访问日志
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<ApiLog> items, int total)> ByApiLogListRequest(ByApiLogListRequest request)
    {
        return await repository.GetByApiLogListAsync(request);
    }
}