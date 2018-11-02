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
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<PhotoGalleryIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("PhotoGalleryDbContextConnection")));

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<PhotoGalleryIdentityDbContext>();
            });
        }
    }
}