namespace Api.Dto.Pest
{

    public class PestDto
    {
        public int PestId { get; set; }
        public string? CommonName { get; set; }
        public string? ScientificName { get; set; }
        public string? Description { get; set; }
        public string? Solution { get; set; }
        public string? Host { get; set; }
        public string? ImageUrl { get; set; }
    }
}