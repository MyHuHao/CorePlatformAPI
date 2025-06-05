using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IDepartmentRepository
{
    // 部门列表分页查询
    Task<(IEnumerable<Department> items, int total)> GetDepartmentPageAsync(ByDepartmentListRequest request);
}