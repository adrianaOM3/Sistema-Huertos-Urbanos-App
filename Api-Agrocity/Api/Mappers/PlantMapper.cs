using Api.Dtos.Plant;
using Api.Models;

namespace Api.Mappers
{
    public static class PlantMapper
    {
        // Mapeo de Plant a PlantDto (para obtener las plantas)
        public static PlantDto ToDto(this Plant plant)
        {
            return new PlantDto
            {
                PlantId = plant.PlantId,
                PlantName = plant.PlantName,

                Description = plant.Description,

            };
        }

        // Mapeo de CreatePlantRequestDto a Plant (para crear nuevas plantas)
        public static Plant ToPlantFromCreateDto(this CreatePlantRequestDto createPlantRequest)
        {
            return new Plant
            {
                PlantName = createPlantRequest.PlantName,

                Description = createPlantRequest.Description,

            };
        }

        // Mapeo de UpdatePlantRequestDto a Plant (para actualizar una planta existente)
        public static Plant ToPlantFromUpdateDto(this UpdatePlantRequestDto updatePlantRequest)
        {
            return new Plant
            {
                PlantName = updatePlantRequest.PlantName,

                Description = updatePlantRequest.Description,

            };
        }

        public static PlantDto ToDto(this ExternalPlantDto external)
        {
            return new PlantDto
            {
                PlantName = external.CommonName,
                Description = external.Description ?? string.Join(", ", external.ScientificName),
                ImageUrl = external.DefaultImage?.RegularUrl
            };
        }
    }
}
