using System.ComponentModel.DataAnnotations;
namespace Api.Dto.Pest
{
    public class CreatePestRequestDto
    {
        [Required]
        [StringLength(255, MinimumLength = 3)]
        public string? CommonName { get; set; }

        [StringLength(255)]
        public string? ScientificName { get; set; }
        public string? Description { get; set; }
        public string? Solution { get; set; }
        public string? Host { get; set; }
        [Url]
        public string? ImageUrl { get; set; }
    }
}