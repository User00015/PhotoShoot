using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Image = PhotoGallery.Entities.Image;

namespace PhotoGallery.Services
{
    public class ImageService : IImageService, IDisposable
    {
        private const string bucketName = "photos-lisamaczink";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;
        private readonly PhotoGalleryIdentityDbContext _context;
        private readonly int SIZE_OF_PAGE_VIEW = 48;
        private readonly AmazonS3Client _s3Client;
        private static readonly Uri BaseUri = new Uri(@"https://s3.us-east-2.amazonaws.com/photos-lisamaczink/");

        public ImageService(PhotoGalleryIdentityDbContext context)
        {
            _context = context;
            _s3Client = new AmazonS3Client(bucketRegion);
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

        public IQueryable<string> GetBannerImages()
        {
            IQueryable<Image> images = _context.Images.AsQueryable();
            return images.OrderByDescending(p => p.TimeStamp).Where(p => p.Type == ImageType.Banner).Take(4).Select(p => p.Url);
        }

        public IQueryable<Image> GetImages(int page, ImageType type)
        {
            return _context.Images.OrderByDescending(p => p.TimeStamp).Where(p => p.Type == type)
                .Skip(page * SIZE_OF_PAGE_VIEW).Take(SIZE_OF_PAGE_VIEW).Select(i => i);
        }

        public async Task DeleteImage(string id)
        {
            using (_context.Database.BeginTransaction())
            {
                var image = await _context.Images.FindAsync(new Guid(id));
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
            foreach (var image in images)
            {
                var guid = Guid.NewGuid();
                UploadToDBContext(image, type, guid);
                UploadToS3(image, guid);
            }
        }

        public async Task UploadToDBContext(IFormFile image, ImageType type, Guid guid)
        {
            var uri = new Uri(BaseUri, guid.ToString());
            await _context.Images.AddAsync(new Image
            {
                FileName = image.FileName,
                Id = guid,
                Url = uri.AbsoluteUri,
                TimeStamp = DateTime.Now,
                Type = type

            });

            await _context.SaveChangesAsync();
        }

        public async Task UploadToS3(IFormFile image, Guid guid)
        {
            var ms = new MemoryStream();
            image.CopyTo(ms);
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = ms,
                Key = guid.ToString(),
                BucketName = bucketName,
                CannedACL = S3CannedACL.PublicRead,
            };

            using (var transferUtility = new TransferUtility(_s3Client))
            {
                await transferUtility.UploadAsync(uploadRequest);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _s3Client.Dispose();
            _context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
