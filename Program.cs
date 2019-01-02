using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace PhotoGallery
{
    public static class Program
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
                    builder.AddSystemsManager("/PhotoGallery");
                })
            .UseKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = null;
                })
                .UseStartup<Startup>();
    }
}