using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PhotoGallery
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            UpdateDbContext.Update(host);

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel(options =>
                {
                    options.Limits.MaxRequestBodySize = null;
                })
                .UseStartup<Startup>();
    }
}