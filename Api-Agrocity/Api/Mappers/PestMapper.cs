using Api.Dtos.Pest;
using Api.Models;

namespace Api.Mappers
{
    public static class PestMapper
    {
        public static PestDto ToPestDto(this Pest pest)
        {
            return new PestDto
            {
                PestId = pest.PestId,
                CommonName = pest.CommonName,
                ScientificName = pest.ScientificName,
                Description = pest.Description,
                Solution = pest.Solution,
                Host = pest.Host,
                ImageUrl = pest.ImageUrl
            };
        }


        // This method maps an ExternalPestDto object to a PestDto object.
        public static PestDto ToPestDto(this ExternalPestDto external)
        {
            return new PestDto
            {
                CommonName = external.common_name,
                ScientificName = external.scientific_name,
                Description = external.description?.FirstOrDefault()?.description,
                Solution = external.solution?.FirstOrDefault()?.description,
                Host = external.host != null ? string.Join(", ", external.host) : null,
                ImageUrl = external.images?.FirstOrDefault()?.regular_url
            };
        }
        public static Pest ToPestFromCreateDto(this CreatePestRequestDto pestDto)
        {
            return new Pest
            {
                CommonName = pestDto.CommonName,
                ScientificName = pestDto.ScientificName,
                Description = pestDto.Description,
                Solution = pestDto.Solution,
                Host = pestDto.Host,
                ImageUrl = pestDto.ImageUrl
            };
        }
    }
}