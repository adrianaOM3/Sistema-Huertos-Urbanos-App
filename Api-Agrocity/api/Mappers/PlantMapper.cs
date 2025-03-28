using api.Dtos.Plant;
using api.Models;

namespace api.Mappers
{
    public static class PlantMapper
    {
        // Mapeo de Plant a PlantDto (para obtener las plantas)
        public static PlantDto ToDto(this Plant plant)
        {
            return new PlantDto
            {
                Id = plant.Id,
                Name = plant.Name,
                ScientificName = plant.ScientificName,
                Description = plant.Description,
                GrowthCycle = plant.GrowthCycle,
                Watering = plant.Watering,
                HardinessZone = plant.HardinessZone,
                HardinessZoneDescription = plant.HardinessZoneDescription,
                Flowers = plant.Flowers,
                Sun = plant.Sun,
                Fruits = plant.Fruits,
                Edible = plant.Edible,
                Leaf = plant.Leaf,
                LeafColor = plant.LeafColor,
                GrowthRate = plant.GrowthRate,
                Maintenance = plant.Maintenance,
                SaltTolerant = plant.SaltTolerant,
                CareLevel = plant.CareLevel
            };
        }

        // Mapeo de CreatePlantRequestDto a Plant (para crear nuevas plantas)
        public static Plant ToPlantFromCreateDto(this CreatePlantRequestDto createPlantRequest)
        {
            return new Plant
            {
                Name = createPlantRequest.Name,
                ScientificName = createPlantRequest.ScientificName,
                Description = createPlantRequest.Description,
                GrowthCycle = createPlantRequest.GrowthCycle,
                Watering = createPlantRequest.Watering,
                HardinessZone = createPlantRequest.HardinessZone,
                HardinessZoneDescription = createPlantRequest.HardinessZoneDescription,
                Flowers = createPlantRequest.Flowers,
                Sun = createPlantRequest.Sun,
                Fruits = createPlantRequest.Fruits,
                Edible = createPlantRequest.Edible,
                Leaf = createPlantRequest.Leaf,
                LeafColor = createPlantRequest.LeafColor,
                GrowthRate = createPlantRequest.GrowthRate,
                Maintenance = createPlantRequest.Maintenance,
                SaltTolerant = createPlantRequest.SaltTolerant,
                CareLevel = createPlantRequest.CareLevel
            };
        }

        // Mapeo de UpdatePlantRequestDto a Plant (para actualizar una planta existente)
        public static Plant ToPlantFromUpdateDto(this UpdatePlantRequestDto updatePlantRequest)
        {
            return new Plant
            {
                Name = updatePlantRequest.Name,
                ScientificName = updatePlantRequest.ScientificName,
                Description = updatePlantRequest.Description,
                GrowthCycle = updatePlantRequest.GrowthCycle,
                Watering = updatePlantRequest.Watering,
                HardinessZone = updatePlantRequest.HardinessZone,
                HardinessZoneDescription = updatePlantRequest.HardinessZoneDescription,
                Flowers = updatePlantRequest.Flowers,
                Sun = updatePlantRequest.Sun,
                Fruits = updatePlantRequest.Fruits,
                Edible = updatePlantRequest.Edible,
                Leaf = updatePlantRequest.Leaf,
                LeafColor = updatePlantRequest.LeafColor,
                GrowthRate = updatePlantRequest.GrowthRate,
                Maintenance = updatePlantRequest.Maintenance,
                SaltTolerant = updatePlantRequest.SaltTolerant,
                CareLevel = updatePlantRequest.CareLevel
            };
        }
    }
}
