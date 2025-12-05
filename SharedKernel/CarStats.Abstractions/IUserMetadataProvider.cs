using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.Abstractions
{
    public interface IUserMetadataProvider
    {
        UserMetadata UserMetadata { get; }
    }

    public class UserMetadata
    {
        public string IpAddress { get; init; }
        public string CorrelationId { get; init; }
        public Guid UserId { get; init; }
    }
}
