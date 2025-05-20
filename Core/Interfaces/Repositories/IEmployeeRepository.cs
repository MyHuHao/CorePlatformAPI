using Core.Contracts.Requests;
using Core.Entities;

namespace Core.Interfaces.Repositories;

public interface IEmployeeRepository
{
    /// <summary>
    /// 通过人员id查询人员信息
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Employee?> GetByEmployeeAsync(ByEmployeeRequest request);

    /// <summary>
    /// 获取人员列表
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<(IEnumerable<Employee> items, int total)> GetByEmployeeListAsync(ByEmployeeListRequest request);

    /// <summary>
    /// 新增人员
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<int> AddEmployeeAsync(AddEmployeeRequest request);

    /// <summary>
    /// 批量新增账号
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    Task<int> BatchAddEmployeeAsync(List<AddEmployeeRequest> list);

    /// <summary>
    /// 删除账号
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task DeleteEmployeeAsync(ByEmployeeRequest request);
}