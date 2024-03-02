using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTOs
{
    public class RegionPutDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must be a minimum of 3 characters")]
        [MaxLength(3, ErrorMessage = "Code must be maximum of 3 characters")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Name must be maximum of 100 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
