using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.Car.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : ControllerBase
    {
        [HttpGet(nameof(GetToyota))]
        public string GetToyota()
        {
            return "Toyota Corolla";
        }
    }
}
