using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class ApiLogQuery(IApiLogRepository repository)
{
    public async Task<(IEnumerable<ApiLog> items, int total)> GetApiLogByPage(ApiLogRequest request)
    {
        return await repository.GetApiLogByPage(request);
    }
}