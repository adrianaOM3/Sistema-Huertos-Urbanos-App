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
                plantId = plant.plantId,
                plantName = plant.plantName,
                ScientificName = plant.ScientificName,
                Description = plant.Description,
                GrowthCycle = plant.GrowthCycle,
                Watering = plant.wateringFrequency,
                HardinessZone = plant.HardinessZone,
                HardinessZoneDescription = plant.HardinessZoneDescription,
                Flowers = plant.flowerDetails,
                Sun = plant.sunExposure,
                Fruits = plant.fruitDetails,
                Edible = plant.isEdible,
                Leaf = plant.hasLeaves,
                LeafColor = plant.LeafColor,
                GrowthRate = plant.GrowthRate,
                Maintenance = plant.maintenanceLevel,
                SaltTolerant = plant.isSaltTolerant,
                CareLevel = plant.CareLevel
            };
        }

        // Mapeo de CreatePlantRequestDto a Plant (para crear nuevas plantas)
        public static Plant ToPlantFromCreateDto(this CreatePlantRequestDto createPlantRequest)
        {
            return new Plant
            {
                plantName = createPlantRequest.plantName,
                ScientificName = createPlantRequest.ScientificName,
                Description = createPlantRequest.Description,
                GrowthCycle = createPlantRequest.GrowthCycle,
                wateringFrequency = createPlantRequest.Watering,
                HardinessZone = createPlantRequest.HardinessZone,
                HardinessZoneDescription = createPlantRequest.HardinessZoneDescription,
                flowerDetails = createPlantRequest.Flowers,
                sunExposure = createPlantRequest.Sun,
                fruitDetails = createPlantRequest.Fruits,
                isEdible = createPlantRequest.Edible,
                hasLeaves = createPlantRequest.Leaf,
                LeafColor = createPlantRequest.LeafColor,
                GrowthRate = createPlantRequest.GrowthRate,
                maintenanceLevel = createPlantRequest.Maintenance,
                isSaltTolerant = createPlantRequest.SaltTolerant,
                CareLevel = createPlantRequest.CareLevel
            };
        }

        // Mapeo de UpdatePlantRequestDto a Plant (para actualizar una planta existente)
        public static Plant ToPlantFromUpdateDto(this UpdatePlantRequestDto updatePlantRequest)
        {
            return new Plant
            {
                plantName = updatePlantRequest.plantName,
                ScientificName = updatePlantRequest.ScientificName,
                Description = updatePlantRequest.Description,
                GrowthCycle = updatePlantRequest.GrowthCycle,
                wateringFrequency = updatePlantRequest.Watering,
                HardinessZone = updatePlantRequest.HardinessZone,
                HardinessZoneDescription = updatePlantRequest.HardinessZoneDescription,
                flowerDetails = updatePlantRequest.Flowers,
                sunExposure = updatePlantRequest.Sun,
                fruitDetails = updatePlantRequest.Fruits,
                isEdible = updatePlantRequest.Edible,
                hasLeaves = updatePlantRequest.Leaf,
                LeafColor = updatePlantRequest.LeafColor,
                GrowthRate = updatePlantRequest.GrowthRate,
                maintenanceLevel = updatePlantRequest.Maintenance,
                isSaltTolerant = updatePlantRequest.SaltTolerant,
                CareLevel = updatePlantRequest.CareLevel
            };
        }
    }
}
