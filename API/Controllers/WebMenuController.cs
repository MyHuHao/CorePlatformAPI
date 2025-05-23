using Core.Contracts.Requests;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class WebMenuController(IWebMenuService service) : Controller
{
    // 菜单分页查询
    [HttpPost]
    public async Task<IActionResult> GetWebMenuByPage([FromBody] ByWebMenuListRequest request)
    {
         var result = await service.GetWebMenuByPageAsync(request);
         return Ok(result);
    }
    
    // 新增菜单
    [HttpPost]
    public async Task<IActionResult> AddWebMenu([FromBody] AddWebMenuRequest request)
    {
        var result = await service.AddWebMenuAsync(request);
        return Ok(result);
    }
    
    // 获取父菜单列表
    [HttpGet]
    public async Task<IActionResult> GetParentWebMenuList()
    {
        var result = await service.GetParentWebMenuListAsync();
        return Ok(result);
    }
}