using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be a 3 character ")]
        [MaxLength(3, ErrorMessage = "Code has to be a 3 character ")]

        public string code { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Only 15 character are entered  ")]

        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
 