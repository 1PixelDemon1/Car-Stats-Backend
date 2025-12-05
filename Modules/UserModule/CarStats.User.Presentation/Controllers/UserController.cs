using CarStats.Abstractions;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUserMetadataProvider _currentUserService;

        public UserController(IUserMetadataProvider currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [Authorize]
        [HttpGet(nameof(GetToyotaOwner))]
        public string GetToyotaOwner()
        {
            var a = _currentUserService.UserMetadata.UserId;
            return "Toyota Corolla Owner";
        }
    }
}
