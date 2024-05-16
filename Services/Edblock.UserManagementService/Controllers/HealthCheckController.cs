using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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