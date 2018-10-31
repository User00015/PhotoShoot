using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace PhotoGallery.Services.Interfaces
{
    public interface IImageService
    {
        Task uploadedRouletteImages(IList<IFormFile> images);
    }
}