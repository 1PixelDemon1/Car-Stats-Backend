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
        /// <param name="moduleName">Имя модуля (папки, где лежит модуль)</param>
        /// <param name="packageName">Имя пакета (папки, где лежит файл с конфигурацией)</param>
        /// <returns>Конфигурация, содержащаяся в Json-файле</returns>
        public static IConfiguration GetModuleConfiguration(string fileName, string moduleName, string packageName)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "/../" + $"Modules/{moduleName}/{packageName}")
                .AddJsonFile($"{fileName}.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"{fileName}.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
