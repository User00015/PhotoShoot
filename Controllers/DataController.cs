using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PhotoGallery.Entities;

namespace PhotoGallery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : Controller
    {
        [HttpGet("ImageTypes")]
        public IEnumerable<ImageType> GetImageTypes()
        {
            //ImageType image = ImageType.Weddings;
            //return JsonConvert.SerializeObject(image);

            return Enum.GetValues(typeof(ImageType)).Cast<ImageType>();
        }

    }


}