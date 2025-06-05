using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class CostCenterQuery(ICostCenterRepository repository)
{
    // 成本中心分页查询
    public async Task<(IEnumerable<CostCenter> items, int total)> GetCostCenterPageAsync(ByCostCenterListRequest request)
    {
        return await repository.GetCostCenterPageAsync(request);
    }
}