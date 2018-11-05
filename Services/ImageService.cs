using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public IEnumerable<FileContentResult> getRouletteImages()
        {
            var imageData = _context.Images.OrderByDescending(p => p.Id).Take(4).Select(p => p.Data);
            foreach(var image in imageData)
            {
                yield return new FileContentResult(image, "image/jpeg");

            }
        }

        public FileContentResult getRouletteImage()
        {
            var image = _context.Images.FirstOrDefault()?.Data;
            return new FileContentResult(image, "image/jpeg");
        }
    }
}
