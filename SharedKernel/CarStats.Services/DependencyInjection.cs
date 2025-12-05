using CarStats.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSharedKernelServices(this IServiceCollection services)
        {
            services.AddScoped<IUserMetadataProvider, UserMetadataProvider>();

            return services;
        }
    }
}
