using Core.Contracts.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class EmployeeController(IEmployeeService service) : Controller
{
    /// <summary>
    ///     通过用户ID获取详情
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> GetEmployeeById([FromBody] ByEmployeeRequest request)
    {
        var result = await service.GetEmployeeById(request);
        return Ok(result);
    }

    /// <summary>
    ///     分页查询
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> GetEmployeePage([FromBody] ByEmployeeListRequest request)
    {
        var result = await service.GetEmployeePageAsync(request);
        return Ok(result);
    }

    // 人员选择分页查询
    [HttpPost]
    public async Task<IActionResult> GetEmployeePageBySelect([FromBody] ByEmployeeListRequest request)
    {
        var result = await service.GetEmployeePageBySelectAsync(request);
        return Ok(result);
    }
    
    // 新增人员
    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeRequest request)
    {
        var result = await service.AddEmployeeAsync(request);
        return Ok(result);
    }
    
    // 修改人员信息
    [HttpPost]
    public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeRequest request)
    {
        var result = await service.UpdateEmployeeAsync(request);
        return Ok(result);
    }
    
    // 删除人员
    [HttpDelete]
    public async Task<IActionResult> DeleteEmployeeById(string id, string companyId)
    {
        var result = await service.DeleteEmployeeByIdAsync(id, companyId);
        return Ok(result);
    }
}