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
        Task uploadedRouletteImages(IFormFileCollection images);
        IEnumerable<FileContentResult> getRouletteImages();
        FileContentResult getRouletteImage();
    }
}