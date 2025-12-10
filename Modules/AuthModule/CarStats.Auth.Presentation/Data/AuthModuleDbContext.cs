using CarStats.Auth.Presentation.Entities;
using CarStats.Utils;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
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
            builder.HasDefaultSchema("AuthModule");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = ModuleLoaderExtensions.GetModuleConfiguration("auth-appsettings");

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreSql"),
                postgresqlOptions => 
                    postgresqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName, "AuthModule"));
        }
    }
}
