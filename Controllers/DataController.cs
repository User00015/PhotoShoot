using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PhotoGallery.Entities;
using PhotoGallery.Helpers;

namespace PhotoGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        private readonly IEnvironment _env;
        private readonly string _foo;

        public DataController(IConfiguration config, IEnvironment env)
        {
            _env = env;
        }

        [HttpGet("Foo")]
        public IActionResult GetFoo()
        {
            return Ok(_env.Url);
        }

        [HttpGet("ImageTypes")]
        public IEnumerable<ImageType> GetImageTypes()
        {
            //ImageType image = ImageType.Weddings;
            //return JsonConvert.SerializeObject(image);

            return Enum.GetValues(typeof(ImageType)).Cast<ImageType>();
        }

    }


}