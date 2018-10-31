using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("home")]
        public IActionResult GetRoulette()
        {
            return Ok("image - get");

        }

        [HttpPost("uploadRoulette")]
        public async Task<IActionResult> UploadRoulette(IList<IFormFile> images)
        {
            if (images == null) return BadRequest("Null File");
            if (images.Count == 0) return BadRequest("Empty File");
            if (ACCEPTED_FILE_TYPES.All(s => s != Path.GetExtension(images.Select(p => p.FileName).ToString()).ToLower())) return BadRequest("Invalid file type.");
            await _imageService.uploadedRouletteImages(images);
           
            return Ok("image - post");
        }

    }
}