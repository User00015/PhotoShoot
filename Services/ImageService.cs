using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<ImageViewModel>> GetImagesAsync(int page, ImageType type)
        {
            var images = _context.Images.OrderByDescending(p => p.TimeStamp).Where(p => p.Type == type)
                .Skip(page * SIZE_OF_PAGE_VIEW).Take(SIZE_OF_PAGE_VIEW).ToList();
            var imageMapping = images.Select(image => Task.Run(() => MapImage(image))).ToList();

            await Task.WhenAll(imageMapping).ConfigureAwait(false);
            return imageMapping.Select(i => i.Result);
        }

        private ImageViewModel MapImage(Image image)
        {
            var bufferedImage = _cache.GetString(image.Id.ToString());

            if (bufferedImage != null)
            {
                return new ImageViewModel()
                {
                    ImageBase64 = bufferedImage,
                    Id = image.Id.ToString()
                };
            }

            var sharpImage = SixLabors.ImageSharp.Image.Load(image.Data);
            sharpImage.Mutate(x => x.Resize(290, 160));
            return new ImageViewModel()
            {
                Id = image.Id.ToString(),
                ImageBase64 = sharpImage.ToBase64String(ImageFormats.Jpeg)
            };
        }

        public async Task DeleteImage(string id)
        {
            using (_context.Database.BeginTransaction())
            {
                var image = await _context.Images.FindAsync(new Guid(id)).ConfigureAwait(false);
                if (image != null)
                {
                    _context.Entry(image).State = EntityState.Deleted;
                }
                _context.SaveChanges();
                _context.Database.CommitTransaction();
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
                        _context.Images.Add(new Image
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

        public string ResizeImage(string imgData)
        {
            var imgBytes = Convert.FromBase64String(imgData.Substring(23)); //Strip the characters 'data:image/jpg;base64,'
            var sharpImage = SixLabors.ImageSharp.Image.Load(imgBytes);
            sharpImage.Mutate(x => x.Resize(290, 160));
            return sharpImage.ToBase64String(ImageFormats.Jpeg);

        }
    }
}
