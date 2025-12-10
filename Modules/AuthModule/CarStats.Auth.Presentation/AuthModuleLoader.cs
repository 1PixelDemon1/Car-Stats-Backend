using CarStats.Abstractions;
using CarStats.Auth.Presentation.Data;
using CarStats.Auth.Presentation.Entities;
using CarStats.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Validation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CarStats.Auth.Presentation
{
    public class AuthModuleLoader : IModuleLoader
    {
        public IServiceCollection Load(IServiceCollection services)
        {
            var configuration = ModuleLoaderExtensions.GetModuleConfiguration("auth-appsettings");

            services.AddDbContext<AuthModuleDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSql"),
                postgresqlOptions => postgresqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "AuthModule")));

            services.AddIdentity<ApplicationUserEntity, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<AuthModuleDbContext>()
                .AddDefaultTokenProviders();

            services.AddOpenIddict()
                .AddCore(options =>
                {
                    options.UseEntityFrameworkCore()
                        .UseDbContext<AuthModuleDbContext>();
                })
                .AddServer(options =>
                {
                    options.SetTokenEndpointUris("/connect/token")
                        .SetUserInfoEndpointUris("/connect/userinfo");

                    options.AllowPasswordFlow()
                        .AllowRefreshTokenFlow();

                    options.RegisterScopes(Scopes.Email, Scopes.Profile, Scopes.Roles);
                    options.AddDevelopmentEncryptionCertificate()
                        .AddDevelopmentSigningCertificate();

                    options.UseAspNetCore()
                        .EnableTokenEndpointPassthrough()
                        //TODO Удалить ес че
                        .DisableTransportSecurityRequirement();
                })
                .AddValidation(options =>
                {
                    options.UseLocalServer();
                    options.UseAspNetCore();
                });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            });

            return services;
        }
    }
}
