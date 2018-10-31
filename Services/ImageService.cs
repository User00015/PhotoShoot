using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PhotoGallery.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ImageService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task uploadedRouletteImages(IList<IFormFile> images)
        {

            string uploadFilesPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFilesPath))
            {
                Directory.CreateDirectory(uploadFilesPath);
            }

            foreach (IFormFile img in images)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                string filePath = Path.Combine(uploadFilesPath, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await img.CopyToAsync(stream);
                }
                var photo = new file { FileName = fileName };
                context.files.Add(photo);
                await context.SaveChangesAsync();
            }
        }
    }
}
