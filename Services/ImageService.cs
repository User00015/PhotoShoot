using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace PhotoGallery.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private PhotoGalleryIdentityDbContext _context;

        public ImageService(IHostingEnvironment hostingEnvironment, PhotoGalleryIdentityDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        //public async Task uploadedRouletteImages(IFormFileCollection images)
        //{

        //    string uploadFilesPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
        //    if (!Directory.Exists(uploadFilesPath))
        //    {
        //        Directory.CreateDirectory(uploadFilesPath);
        //    }

        //    foreach (var img in images)
        //    {
        //        string fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
        //        string filePath = Path.Combine(uploadFilesPath, fileName);
        //        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            img.CopyTo(stream);
        //        }
        //        var photo = new Images { FileName = fileName };
        //        _context.Images.Add(photo);
        //    }
        //    await _context.SaveChangesAsync();
        //}

        public async Task uploadedRouletteImages(IFormFileCollection images)
        {
            using (IDbContextTransaction db = _context.Database.BeginTransaction())
            {
                foreach (IFormFile image in images)
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        _context.Images.Add(new Image
                        {
                            FileName = image.FileName,
                            Id = Guid.NewGuid(),
                            Data = ms.ToArray()
                        });
                    }
                }
                db.Commit();
            }
            await _context.SaveChangesAsync();
        }

        public int getRouletteImages()
        {
            return _context.Images.Count();
        }
    }
}
