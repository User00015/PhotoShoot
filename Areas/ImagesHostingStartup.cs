using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhotoGallery.Areas.Identity.Data;

[assembly: HostingStartup(typeof(PhotoGallery.Areas.ImagesHostingStartup))]
namespace PhotoGallery.Areas
{
    public class ImagesHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ImagesContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("PhotoGalleryDbContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>()
                //    .AddEntityFrameworkStores<PhotoGalleryIdentityDbContext>();
            });
        }
    }
}