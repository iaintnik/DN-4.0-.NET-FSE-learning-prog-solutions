using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,POC")] // only users with Admin or POC role
    public class EmployeeController : ControllerBase
    {
        [HttpGet("data")]
        public IActionResult Get()
        {
            return Ok("This is protected Employee data.");
        }
    }
}
