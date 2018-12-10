using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using NLog.Config;
using NLog.Extensions.Logging;

namespace NLog.AspNetCore.TargetGelf
{
    public static class GelfWebHostBuilderExtensions
    {
        /// <summary>Use NLog Target Gelf for Dependency Injected loggers.</summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseNLogTargetGelf(this IWebHostBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.ConfigureServices(services =>
            {
                ConfigurationItemFactory.Default.RegisterItemsFromAssembly(typeof(GelfConfigurationExtensions).GetTypeInfo().Assembly);
                LogManager.AddHiddenAssembly(typeof(GelfConfigurationExtensions).GetTypeInfo().Assembly);
            });

            return builder;
        }
    }
}
