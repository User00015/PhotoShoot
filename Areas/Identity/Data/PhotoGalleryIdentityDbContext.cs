using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Entities;
using WebApi.Entities;

namespace PhotoGallery.Areas.Identity.Data
{
    public class PhotoGalleryIdentityDbContext : IdentityDbContext<User>
    {
        public PhotoGalleryIdentityDbContext(DbContextOptions<PhotoGalleryIdentityDbContext> options) : base(options)
        {
        }

        //public DbSet<RouletteImage> RouletteImages { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Image>().ToTable("Images");
            builder.Entity<Event>().ToTable("Events");
            //builder.Entity<GalleryImage>().ToTable("GalleryImages");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }

}
