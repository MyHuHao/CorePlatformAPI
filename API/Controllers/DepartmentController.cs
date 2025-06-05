using Core.Contracts.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class DepartmentController(IDepartmentService service) : Controller
{
    // 部门列表分页查询
    [HttpPost]
    public async Task<IActionResult> GetDepartmentPage([FromBody] ByDepartmentListRequest request)
    {
        var result = await service.GetDepartmentPageAsync(request);
        return Ok(result);
    }
}