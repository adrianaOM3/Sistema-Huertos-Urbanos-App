using System.Text.Json.Serialization;

namespace Api.Dtos.Plant
{
    public class ExternalPlantDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("common_name")]
        public string CommonName { get; set; }

        [JsonPropertyName("scientific_name")]
        public List<string> ScientificName { get; set; }

        [JsonPropertyName("default_image")]
        public PlantImage DefaultImage { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; } // Solo para detalles individuales

    }

    public class PlantImage
    {
        [JsonPropertyName("regular_url")]
        public string RegularUrl { get; set; }
    }

    public class ExternalPlantWrapper
    {
        public List<ExternalPlantDto> data { get; set; }
    }
}
