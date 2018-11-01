using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoGallery.Areas;
using PhotoGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = PhotoGallery.Entities.File;

namespace PhotoGallery.Services
{
    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ImagesContext _context;

        public ImageService(IHostingEnvironment hostingEnvironment, ImagesContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }
        public async Task uploadedRouletteImages(IFormFileCollection images)
        {

            string uploadFilesPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFilesPath))
            {
                Directory.CreateDirectory(uploadFilesPath);
            }

            foreach (var img in images)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(img.FileName);
                string filePath = Path.Combine(uploadFilesPath, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    img.CopyTo(stream);
                }
                var photo = new File { FileName = fileName };
                _context.Files.Add(photo);
            }
            await _context.SaveChangesAsync();
        }

        public IEnumerable<File> getRouletteImages()
        {
            return _context.Files.Local.Select(file => file);
        }
    }
}
