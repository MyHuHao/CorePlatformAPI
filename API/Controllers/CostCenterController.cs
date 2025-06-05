using Core.Contracts.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class CostCenterController(ICostCenterService service) : Controller
{
    // 成本中心分页查询
    [HttpPost]
    public async Task<IActionResult> GetCostCenterPage([FromBody] ByCostCenterListRequest request)
    {
        var result = await service.GetCostCenterPageAsync(request);
        return Ok(result);
    }
}