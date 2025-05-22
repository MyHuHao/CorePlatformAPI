using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class AccountController : ControllerBase
{
    
}