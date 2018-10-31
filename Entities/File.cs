using System.ComponentModel.DataAnnotations;

namespace PhotoGallery.Entities
{
    public class File
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
    }
}