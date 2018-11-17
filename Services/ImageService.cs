using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.Extensions.Caching.Distributed;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Image = PhotoGallery.Entities.Image;

namespace PhotoGallery.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly PhotoGalleryIdentityDbContext _context;
        private readonly int SIZE_OF_PAGE_VIEW = 48;
        private readonly IDistributedCache _cache;

        public ImageService(IHostingEnvironment hostingEnvironment, PhotoGalleryIdentityDbContext context, IDistributedCache cache)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _cache = cache;
        }

        public void DeleteEntireGallery()
        {
            using (_context.Database.BeginTransaction())
            {
                foreach (var img in _context.Images)
                {
                    _context.Entry(img).State = EntityState.Deleted;

                }
                _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
        }

        public IEnumerable<string> GetBannerImages()
        {
            IQueryable<Image> images = _context.Images.AsQueryable();
            return images.OrderByDescending(p => p.TimeStamp).Where(p => p.Type == ImageType.Banner).Take(4).Select(p => $"data:image/jpg;base64, {Convert.ToBase64String(p.Data)}");
        }

        public IEnumerable<string> GetImages(int page, ImageType type)
        {
            IQueryable<Image> images = _context.Images.AsQueryable();
            foreach (var img in images.OrderByDescending(p => p.TimeStamp).Where(p => p.Type == type).Skip(page * SIZE_OF_PAGE_VIEW).Take(SIZE_OF_PAGE_VIEW))
            {
                var image = SixLabors.ImageSharp.Image.Load(img.Data);
                image.Mutate(x => x.Resize(290, 160));
                yield return image.ToBase64String(ImageFormats.Jpeg);

            }
        }


        public void UploadImages(IFormFileCollection images, ImageType type)
        {
            using (_context.Database.BeginTransaction())
            {
                foreach (IFormFile image in images)
                {

                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.CopyTo(ms);
                        _context.Images.Add(new Image()
                        {
                            FileName = image.FileName,
                            Id = Guid.NewGuid(),
                            Data = ms.ToArray(),
                            TimeStamp = DateTime.Now,
                            Type = type

                        });
                    }
                }
                _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
        }
    }
}
