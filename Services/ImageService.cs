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

namespace PhotoGallery.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly PhotoGalleryIdentityDbContext _context;
        private readonly int SIZE_OF_PAGE_VIEW = 48;

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

        public void DeleteEntireGallery()
        {
            using (_context.Database.BeginTransaction())
            {
                foreach (var img in _context.GalleryImages)
                {
                    _context.Entry(img).State = EntityState.Deleted;

                }
                _context.SaveChanges();
                _context.Database.CommitTransaction();
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
            IQueryable<RouletteImage> images = _context.RouletteImages.AsQueryable();
            return images.OrderByDescending(p => p.TimeStamp).Take(4).Select(p => $"data:image/jpg;base64, {Convert.ToBase64String(p.Data)}");
        }

        public IEnumerable<string> GetGalleryImages(int page)
        {
            IQueryable<GalleryImage> images = _context.GalleryImages.AsQueryable();
            return images.OrderByDescending(p => p.TimeStamp).Skip(page * SIZE_OF_PAGE_VIEW).Take(SIZE_OF_PAGE_VIEW).Select(p => $"data:image/jpg;base64, {Convert.ToBase64String(p.Data)}");
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
