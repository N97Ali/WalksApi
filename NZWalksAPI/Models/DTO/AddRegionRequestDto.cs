using System.ComponentModel.DataAnnotations;

namespace NZWalksAPI.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3,ErrorMessage ="Code has to be a 3 character ")]
        [MaxLength(3, ErrorMessage =  "Max length is only 3 character")]
        public string code { get; set; }
        [Required]
         
        [MaxLength(15, ErrorMessage = "Code has to be a 15 character ")]

        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
