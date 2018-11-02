using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        [Required]
        public byte[] Data { get; set; }
    }
}