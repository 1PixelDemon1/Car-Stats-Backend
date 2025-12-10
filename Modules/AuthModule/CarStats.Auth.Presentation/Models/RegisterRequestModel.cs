using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.Auth.Presentation.Models
{
    public class RegisterRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
