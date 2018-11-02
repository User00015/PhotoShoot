using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Newtonsoft.Json;
using PhotoGallery.Services.Interfaces;

namespace PhotoGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] { ".jpg", ".jpeg", ".png" };

        public ImagesController(IImageService imageService, IHostingEnvironment hostingEnvironment)
        {
            _imageService = imageService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("roulette")]
        public IEnumerable<FileContentResult> GetRoulette()
        {
            var numberOfImages = _imageService.getRouletteImages();

            return numberOfImages;

        }

        [HttpPost("uploadRoulette")]
        public IActionResult UploadRoulette()
        {
            var files = Request.Form.Files;
            _imageService.uploadedRouletteImages(files);
            return Ok();
        }

    }
}