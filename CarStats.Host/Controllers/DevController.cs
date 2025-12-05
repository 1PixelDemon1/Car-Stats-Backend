using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarStats.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevController : ControllerBase
    {
        [Authorize]
        [HttpGet("Test")]
        public string Test(string value)
        {
            return $"Test: {value}";
        }
    }
}
