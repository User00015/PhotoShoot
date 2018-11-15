﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Entities;
using PhotoGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Primitives;

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

        [HttpPost("test")]
        public int Test()
        {
            IFormFileCollection files = Request.Form.Files;

            return files.Count;
        }

        [Authorize]
        [HttpPost("uploadGallery")]
        public IActionResult UploadToGallery()
        {
            IFormFileCollection files = Request.Form.Files;
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

        [Authorize]
        [HttpPost("uploadRoulette")]
        public IActionResult UploadRoulette()
        {
            IFormFileCollection files = Request.Form.Files;
            _imageService.UploadImages(files, ImageStrategy.Roulette);
            return Ok();
        }

        [Authorize]
        [HttpDelete("deleteGallery")]
        public IActionResult DeleteEntireGallery()
        {
            _imageService.DeleteEntireGallery();
            return Ok("Deleted all gallery images");

        }

    }
}