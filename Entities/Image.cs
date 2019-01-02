using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PhotoGallery.Areas.Identity.Data;

namespace PhotoGallery.Entities
{
    public class Image : IImage
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        [Required]
        public string Url { get; set; }

        public DateTime TimeStamp { get; set; }
        public ImageType Type { get; set; }
    }

    public interface IImage
    {
        Guid Id { get; set; }
        string FileName { get; set; }
        string Url  { get; set; }
        DateTime TimeStamp { get; set; }

        ImageType Type { get; set; }
    }
}