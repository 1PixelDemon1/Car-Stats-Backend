using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.Abstractions
{
    public interface IModuleLoader
    {
        IServiceCollection Load(IServiceCollection services);
    }
}
