using CarStats.Auth.Presentation.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.Auth.Presentation.Data
{
    public class AuthModuleDbContext : IdentityDbContext<ApplicationUserEntity>
    {
        public AuthModuleDbContext(DbContextOptions<AuthModuleDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.UseOpenIddict();
        }
    }
}
