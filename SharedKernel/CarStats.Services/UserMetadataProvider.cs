using CarStats.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.Services
{
    internal class UserMetadataProvider : IUserMetadataProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserMetadataProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        private UserMetadata? _userMetadata;
        public UserMetadata UserMetadata => _userMetadata ?? GetUserMetadata();

        private UserMetadata GetUserMetadata()
        {
            var userId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value);
            var correlationId = _httpContextAccessor.HttpContext.TraceIdentifier;
            var userIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            return new UserMetadata
            {
                UserId = userId,
                CorrelationId = correlationId,
                IpAddress = userIpAddress
            };
        }
    }
}
