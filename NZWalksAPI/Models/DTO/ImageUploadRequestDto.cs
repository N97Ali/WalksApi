using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [Required]
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }

    }
}
