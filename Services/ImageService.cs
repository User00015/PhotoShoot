using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoGallery.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly PhotoGalleryIdentityDbContext _context;

        public ImageService(IHostingEnvironment hostingEnvironment, PhotoGalleryIdentityDbContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        public void UploadImages(IFormFileCollection images, ImageStrategy strategy)
        {
            switch (strategy)
            {
                case ImageStrategy.Gallery:
                    UploadGalleryImages(images);
                    break;
                case ImageStrategy.Roulette:
                    UploadRouletteImages(images);
                    break;
                default:
                    throw new NotImplementedException();
            }

        }

        private void UploadRouletteImages(IFormFileCollection images)
        {
            using (_context.Database.BeginTransaction())
            {
                foreach (IFormFile image in images)
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        _context.RouletteImages.Add(new RouletteImage
                        {
                            FileName = image.FileName,
                            Id = Guid.NewGuid(),
                            Data = ms.ToArray(),
                            TimeStamp = DateTime.Now
                        });
                    }
                }
                _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
        }

        public IEnumerable<string> GetRouletteImages()
        {
            foreach (var image in _context.RouletteImages.OrderByDescending(p => p.TimeStamp).Take(4).Select(p => p.Data))
            {
                yield return $"data:image/jpg;base64,{Convert.ToBase64String(image)}";
            }
        }

        public IEnumerable<string> GetGalleryImages(int size)
        {
            foreach (var image in _context.GalleryImages.Skip(size).Take(1).Select(p => p.Data))
            {
                yield return $"data:image/jpg;base64,{Convert.ToBase64String(image)}";
            }
        }


        private void UploadGalleryImages(IFormFileCollection images)
        {
            using (_context.Database.BeginTransaction())
            {
                foreach (IFormFile image in images)
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        _context.GalleryImages.Add(new GalleryImage()
                        {
                            FileName = image.FileName,
                            Id = Guid.NewGuid(),
                            Data = ms.ToArray(),
                            TimeStamp = DateTime.Now
                        });
                    }
                }
                _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
        }
    }
}
