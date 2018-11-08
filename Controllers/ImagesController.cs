﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using Newtonsoft.Json;
using PhotoGallery.Entities;
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

        [HttpGet("test")]
        public DateTime Test()
        {
            return DateTime.Now;
        }

        [HttpPost("uploadGallery")]
        public IActionResult UploadToGallery()
        {
            var files = Request.Form.Files;
            _imageService.UploadImages(files, ImageStrategy.Gallery);
            return Ok();
        }

        [HttpGet("Gallery")]
        public IEnumerable<string> GetGallery(int size)
        {
            return _imageService.GetGalleryImages(size);
        }

        [HttpGet("roulette")]
        public IEnumerable<string> GetRoulette()
        {
            return _imageService.GetRouletteImages();
        }

        [HttpPost("uploadRoulette")]
        public IActionResult UploadRoulette()
        {
            var files = Request.Form.Files;
            _imageService.UploadImages(files, ImageStrategy.Roulette);
            return Ok();
        }

    }
}