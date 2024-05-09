using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edblock.UserManagementService.Controllers;

[ApiController]
[Route("[controller]")]
[AllowAnonymous]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public string Check() => 
        "Service is online";
}