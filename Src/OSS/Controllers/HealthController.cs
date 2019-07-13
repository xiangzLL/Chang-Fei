using Microsoft.AspNetCore.Mvc;

namespace OSS.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class HealthController:ControllerBase
    {
        [HttpGet("/healthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
