using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using NLog.Config;

namespace NLog.AspNetCore.TargetGelf
{
    public static class GelfConfigurationExtensions
    {
        /// <summary>Apply NLog Gelf configuration from XML config.</summary>
        /// <param name="env"></param>
        /// <param name="configFileRelativePath">relative path to NLog configuration file.</param>
        /// <returns>LoggingConfiguration for chaining</returns>
        public static LoggingConfiguration ConfigureNLog(this IHostingEnvironment env, string configFileRelativePath)
        {
            ConfigurationItemFactory.Default.RegisterItemsFromAssembly(typeof(GelfConfigurationExtensions).GetTypeInfo().Assembly);
            LogManager.AddHiddenAssembly(typeof(GelfConfigurationExtensions).GetTypeInfo().Assembly);
            LogManager.LoadConfiguration(Path.Combine(env.ContentRootPath, configFileRelativePath));
            return LogManager.Configuration;
        }
    }
}
