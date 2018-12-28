using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using PhotoGallery.Areas.Identity.Data;

namespace PhotoGallery
{
    internal static class UpdateDbContext
    {
        public static void Update(IWebHost host)
        {
            var services = (IServiceScopeFactory) host.Services.GetService(typeof(IServiceScopeFactory));

            using (var scope = services.CreateScope())
            {
                var db = GetDatabase(scope);
                db.Database.Migrate();
            }
        }

        private static PhotoGalleryIdentityDbContext GetDatabase(IServiceScope services)
        {
            var db = services.ServiceProvider.GetRequiredService<PhotoGalleryIdentityDbContext>();
            return db;
        }
    }
}