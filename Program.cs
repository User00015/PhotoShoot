using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace PhotoGallery
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args)
                .Build();

            UpdateDbContext.Update(host);

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, builder) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    builder.AddSystemsManager("/PhotoGallery");
                    builder.AddJsonFile($"appsettings.{env.EnvironmentName}.json");
                })
            .UseKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = null;
                })
                .UseStartup<Startup>();
    }
}