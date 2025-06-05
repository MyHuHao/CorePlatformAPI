using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;

namespace Core.Interfaces.Services;

public interface ICostCenterService
{
    // 成本中心分页查询
    Task<ApiResult<PagedResult<CostCenterDto>>> GetCostCenterPageAsync(ByCostCenterListRequest request);
}