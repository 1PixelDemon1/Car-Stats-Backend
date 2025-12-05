using CarStats.Auth.Presentation.Entities;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace CarStats.Auth.Presentation.Controllers
{
    [ApiController]
    [Route("connect")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUserEntity> _userManager;

        public AuthController(UserManager<ApplicationUserEntity> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("~/connect/token")]
        [Produces("application/json")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest() ??
                          throw new InvalidOperationException("Не удалось получить запрос OpenID Connect.");

            if (request.IsPasswordGrantType())
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    return BadRequest(new OpenIddictResponse
                    {
                        Error = Errors.InvalidGrant,
                        ErrorDescription = "Неверное имя пользователя или пароль."
                    });
                }

                var claims = new List<Claim>
                {
                    new(Claims.Subject, user.Id),
                    new(Claims.Username, user.UserName!),
                    new(Claims.Email, user.Email!),
                    new(Claims.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                };

                // Добавляем роли
                var roles = await _userManager.GetRolesAsync(user);
                claims.AddRange(roles.Select(role => new Claim(Claims.Role, role)));

                var identity = new ClaimsIdentity(claims, "token");
                var principal = new ClaimsPrincipal(identity);

                // Устанавливаем срок действия access token (по умолчанию — 1 час)
                // и refresh token (по умолчанию — 14 дней)
                // Можно настроить через options.SetAccessTokenLifetime(...) в Program.cs

                return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            if (request.IsRefreshTokenGrantType())
            {
                // OpenIddict автоматически проверяет refresh token — достаточно вернуть текущего пользователя
                var user = await _userManager.GetUserAsync(User);
                if (user is null)
                {
                    return BadRequest(new OpenIddictResponse
                    {
                        Error = Errors.InvalidGrant,
                        ErrorDescription = "Пользователь не найден."
                    });
                }

                var claims = new List<Claim>
                {
                    new(Claims.Subject, user.Id),
                    new(Claims.Username, user.UserName!),
                    new(Claims.Email, user.Email!)
                };

                var roles = await _userManager.GetRolesAsync(user);
                claims.AddRange(roles.Select(role => new Claim(Claims.Role, role)));

                var identity = new ClaimsIdentity(claims, "token");
                var principal = new ClaimsPrincipal(identity);

                return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            return BadRequest(new OpenIddictResponse
            {
                Error = Errors.UnsupportedGrantType,
                ErrorDescription = "Тип гранта не поддерживается."
            });
        }
    }
}
