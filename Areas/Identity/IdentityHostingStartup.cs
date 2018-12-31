using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoGallery.Areas.Identity.Data;
using WebApi.Entities;

[assembly: HostingStartup(typeof(PhotoGallery.Areas.Identity.IdentityHostingStartup))]
namespace PhotoGallery.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                var connectionString = string.Empty;
                if (context.HostingEnvironment.IsDevelopment())
                {
                    connectionString = context.Configuration.GetConnectionString("LocalhostConnection");
                }

                if (context.HostingEnvironment.IsProduction())
                {
                    connectionString = context.Configuration.GetConnectionString("PhotoGalleryDbContextConnection");
                }

                services.AddDbContext<PhotoGalleryIdentityDbContext>(options =>
                    options.UseSqlServer(connectionString));

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<PhotoGalleryIdentityDbContext>();
            });
        }
    }
}