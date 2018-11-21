﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoGallery.Entities;
using PhotoGallery.Extensions;
using PhotoGallery.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PhotoGallery.Services;

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
        public IActionResult Test(string foo)
        {

            return Ok(foo);
        }

        [Authorize]
        [HttpPost("uploadImages")]
        public IActionResult UploadToGallery(string type)
        {
            ImageType imageType = Enum.Parse<ImageType>(type.RemoveWhiteSpace(), ignoreCase: true);
            IFormFileCollection files = Request.Form.Files;
            _imageService.UploadImages(files, imageType);
            return Ok();
        }

        [HttpGet("getImages")]
        public async Task<IEnumerable<ImageViewModel>> GetImages(int size, string type)
        {
            ImageType imageType = Enum.Parse<ImageType>(type.RemoveWhiteSpace(), ignoreCase: true);
            return await _imageService.GetImagesAsync(size, imageType);
        }


        [HttpGet("banner")]
        public IEnumerable<string> GetBanner()
        {
            return _imageService.GetBannerImages();
        }

        [Authorize]
        [HttpDelete("deleteImage")]
        public async Task<IActionResult> DeleteImage(string id)
        {
            await _imageService.DeleteImage(id);
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