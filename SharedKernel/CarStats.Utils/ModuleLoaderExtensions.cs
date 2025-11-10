using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarStats.Utils
{
    public static class ModuleLoaderExtensions
    {
        /// <summary>
        /// Получить настройку из файла конфигурации модуля.
        /// </summary>
        /// <param name="fileName">Имя Json-файла с конфигурацией</param>
        /// <returns>Конфигурация, содержащаяся в Json-файле</returns>
        public static IConfiguration GetModuleConfiguration(string fileName)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            var path = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                .AddJsonFile($"{fileName}.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{fileName}.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
