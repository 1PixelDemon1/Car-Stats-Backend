using CarStats.Auth.Presentation.Data;
using CarStats.Auth.Presentation.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Abstractions;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CarStats.Auth.Presentation
{
    public static class DefaultAuthValuesInitializer
    {
        public static async Task<WebApplication> InitializeAuthValuesAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AuthModuleDbContext>();

            await context.Database.EnsureCreatedAsync();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUserEntity>>();
            if (await userManager.FindByNameAsync("admin@example.com") == null)
            {
                var user = new ApplicationUserEntity { UserName = "admin@example.com", Email = "admin@example.com" };
                await userManager.CreateAsync(user, "Password123!");
                await userManager.AddToRoleAsync(user, "User");
            }

            var applicationManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();
            if (await applicationManager.FindByClientIdAsync("web_client") == null)
            {
                await applicationManager.CreateAsync(new OpenIddictApplicationDescriptor
                {
                    ClientId = "web_client",
                    ClientSecret = "web_secret",
                    DisplayName = "Web SPA Client",
                    Permissions =
                    {
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.Password,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles
                    }
                });
            }

            return app;
        }
    }
}
