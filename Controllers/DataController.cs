using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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
        private IOptions<AppSettings> _app;

        public DataController(IOptions<AppSettings> settings)
        {
            _app = settings;
        }

        [HttpGet("Foo")]
        public IActionResult GetFoo()
        {
            return new JsonResult(_app);
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