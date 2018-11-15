using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Entities;

namespace PhotoGallery.Services.Interfaces
{
    public interface IImageService
    {
        IEnumerable<string> GetRouletteImages();
        IEnumerable<string> GetGalleryImages(int page);
        void UploadImages(IFormFileCollection files, ImageStrategy strategy);
        void DeleteEntireGallery();
    }
}