using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class DepartmentQuery(IDepartmentRepository repository)
{
    // 部门列表分页查询
    public async Task<(IEnumerable<Department> items, int total)> GetDepartmentPageAsync(ByDepartmentListRequest request)
    {
        return await repository.GetDepartmentPageAsync(request);
    }
}