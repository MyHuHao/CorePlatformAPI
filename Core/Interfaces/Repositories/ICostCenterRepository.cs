using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface ICostCenterRepository
{
    // 成本中心分页查询
    Task<(IEnumerable<CostCenter> items, int total)> GetCostCenterPageAsync(ByCostCenterListRequest request);
}