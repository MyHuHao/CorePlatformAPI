using Core.Contracts;
using Core.Contracts.Requests;
using Core.DTOs;

namespace Core.Interfaces.Services;

public interface IDepartmentService
{
    // 部门列表分页查询
    Task<ApiResult<PagedResult<DepartmentDto>>> GetDepartmentPageAsync(ByDepartmentListRequest request);
}