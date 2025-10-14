using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.User.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet(nameof(GetToyotaOwner))]
        public string GetToyotaOwner()
        {
            return "Toyota Corolla Owner";
        }
    }
}
