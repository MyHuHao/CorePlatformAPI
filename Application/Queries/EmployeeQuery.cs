using Core.Contracts.Requests;
using Core.Contracts.Results;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class EmployeeQuery(IEmployeeRepository repository)
{
    /// <summary>
    ///     通过用户ID获取详情
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Employee?> GetEmployeeById(ByEmployeeRequest request)
    {
        return await repository.GetByEmployeeAsync(request);
    }

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<(IEnumerable<Employee> items, int total)> GetEmployeePageAsync(ByEmployeeListRequest request)
    {
        return await repository.GetByEmployeeListAsync(request);
    }
    
    // 人员选择分页查询
    public async Task<(IEnumerable<EmployeeChangeResult> items, int total)> GetEmployeePageBySelectAsync(ByEmployeeListRequest request)
    {
        return await repository.GetEmployeePageBySelectAsync(request);
    }
}