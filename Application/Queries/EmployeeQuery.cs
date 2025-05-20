using Core.Contracts.Requests;
using Core.Entities;
using Core.Interfaces.Repositories;

namespace Application.Queries;

public class EmployeeQuery(IEmployeeRepository repository)
{
    /// <summary>
    /// 通过用户ID获取详情
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<Employee?> GetEmployeeById(ByEmployeeRequest request)
    {
        return await repository.GetByEmployeeAsync(request);
    }
}