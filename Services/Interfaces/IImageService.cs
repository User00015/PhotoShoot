using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Entities;

namespace PhotoGallery.Services.Interfaces
{
    public interface IImageService
    {
        void UploadImages(IFormFileCollection files, ImageType type);
        void DeleteEntireGallery();
        IQueryable<string> GetBannerImages();
        IQueryable<Image> GetImages(int size, ImageType imageType);
        Task DeleteImage(string id);
    }
}