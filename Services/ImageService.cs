using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PhotoGallery.Areas.Identity.Data;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Image = PhotoGallery.Entities.Image;

namespace PhotoGallery.Services
{
    public class ImageService : IImageService
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
            var image = await _context.Images.FindAsync(new Guid(id));
            if (image != null)
            {
                _context.Entry(image).State = EntityState.Deleted;
            }
            _context.SaveChanges();

            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = id
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
        }


        public int UploadImages(IFormFileCollection images, ImageType type)
        {
            var guids = images.Select(formFile => Guid.NewGuid()).ToList();

            var imagesZip = images.Zip(guids, (file, guid) => new UploadImage()
            {
                Id = guid,
                ImageUpload = file
            }).ToList();

            HandleUploadToS3(imagesZip);
            return HandleUploadToDB(imagesZip, type);
        }


        private int HandleUploadToDB(IEnumerable<UploadImage> imagesZip, ImageType type)
        {

            foreach (var zip in imagesZip)
            {
                _context.Images.AddAsync(new Image
                {
                    FileName = zip.ImageUpload.FileName,
                    Id = zip.Id,
                    Url = new Uri(BaseUri, zip.Id.ToString()).AbsoluteUri,
                    TimeStamp = DateTime.Now,
                    Type = type
                });
            }

            try
            {
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _context.Database.RollbackTransaction();
                _context.Dispose();
                throw;
            }
        }


        private void HandleUploadToS3(IEnumerable<UploadImage> imagesZip)
        {
            imagesZip.ToList().ForEach(async t => await HandleS3Upload(t.ImageUpload, t.Id));
        }

        private async Task HandleS3Upload(IFormFile file, Guid guid)
        {
            using (var transferUtility = new TransferUtility(_s3Client))
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = ms,
                        Key = guid.ToString(),
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.PublicRead,
                    };

                    await transferUtility.UploadAsync(uploadRequest);
                }
            }
        }

        private async Task HandleDBUpload(IFormFile file, Guid guid, ImageType type)
        {
            var uri = new Uri(BaseUri, guid.ToString());
            await _context.Images.AddAsync(new Image
            {
                FileName = file.FileName,
                Id = guid,
                Url = uri.AbsoluteUri,
                TimeStamp = DateTime.Now,
                Type = type

            });

            await _context.SaveChangesAsync();
        }
    }


    internal class UploadImage : IUploadImage
    {
        public IFormFile ImageUpload { get; set; }
        public Guid Id { get; set; }
    }

    internal interface IUploadImage
    {
        IFormFile ImageUpload { get; set; }
        Guid Id { get; set; }
    }
}
