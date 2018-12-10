using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.AspNetCore.TargetGelf;

namespace BasicAspNetCoreApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseNLogTargetGelf()
                .UseStartup<Startup>();
    }
}
