using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.Auth.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet(nameof(GetToyotaOwnerJwt))]
        public string GetToyotaOwnerJwt()
        {
            return "TestJwt";
        }
    }
}
