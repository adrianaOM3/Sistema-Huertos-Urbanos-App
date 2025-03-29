using Api.Dto.Pest;
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