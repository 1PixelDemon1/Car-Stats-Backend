using CarStats.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.User.Presentation
{
    public class UserModuleLoader : IModuleLoader
    {
        public IServiceCollection Load(IServiceCollection services)
        {
            return services;
        }
    }
}
