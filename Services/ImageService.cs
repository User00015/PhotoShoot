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


        public async Task UploadImages(IFormFileCollection images, ImageType type)
        {
            var guids = new List<Guid>();
            foreach (var formFile in images)
            {
                guids.Add(Guid.NewGuid());
            }

            UploadToS3(images, guids);

            //foreach (var image in images)
            //{
            //    var guid = Guid.NewGuid();
            //    UploadToDBContext(image, type, guid);
            //    UploadToS3(image, guid);
            //}
        }

        private void UploadToS3(IFormFileCollection images, IEnumerable<Guid> guids)
        {
            var imagesZip = images.Zip(guids, (file, guid) => new S3Image()
            {
                id = guid.ToString(),
                imageUpload = file
            }).ToList();

            imagesZip.ForEach(async t => await HandleS3Upload(t.imageUpload, t.id));

        }

        private async Task HandleS3Upload(IFormFile file, string guid)
        {
            using (var transferUtility = new TransferUtility(_s3Client))
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = ms,
                        Key = guid,
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.PublicRead,
                    };

                    await transferUtility.UploadAsync(uploadRequest);
                }
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

    }

    public class S3Image
    {
        public IFormFile imageUpload { get; set; }
        public string id { get; set; }
        //public Action<IFormFile, string> UploadDelegate { get; set; }
    }
}
