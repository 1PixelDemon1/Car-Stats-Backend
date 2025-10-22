using CarStats.Abstractions;
using CarStats.Utils;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.Car.Presentation
{
    public class CarModuleLoader : IModuleLoader
    {
        public IServiceCollection Load(IServiceCollection services)
        {

            //ModuleLoaderExtensions.GetModuleConfiguration(
            //        fileName: "car-appsettings",
            //        moduleName: "/.",
            //        packageName: "/.");

            return services;
        }
    }
}
