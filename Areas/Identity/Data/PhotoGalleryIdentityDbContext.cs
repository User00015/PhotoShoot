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
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Event>().OwnsOne(p => p.StartDate);
            builder.Entity<Event>().OwnsOne(p => p.EndDate);
            builder.Entity<Event>().OwnsOne(p => p.StartTime);
            builder.Entity<Event>().OwnsOne(p => p.EndTime);
            builder.Entity<Event>().HasMany(p => p.Appointments);
            //builder.Entity<Event>().HasMany(p => p.Appointments).WithOne(e => e.Event).IsRequired();

            builder.Entity<Image>().ToTable("Images");
            builder.Entity<Event>().ToTable("Events");
            //builder.Entity<GalleryImage>().ToTable("GalleryImages");
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }

}
