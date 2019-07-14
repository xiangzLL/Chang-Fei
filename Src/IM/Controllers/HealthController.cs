using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IM.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet("/healthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
