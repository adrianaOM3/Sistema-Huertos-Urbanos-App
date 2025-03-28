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
                plantId = plant.PlantId,
                plantName = plant.PlantName,
                ScientificName = plant.ScientificName,
                Description = plant.Description,
                GrowthCycle = plant.GrowthCycle,
                Watering = plant.WateringFrequency,
                HardinessZone = plant.HardinessZone,
                HardinessZoneDescription = plant.HardinessZoneDescription,
                Flowers = plant.FlowerDetails,
                Sun = plant.SunExposure,
                Fruits = plant.FruitDetails,
                Edible = plant.IsEdible,
                Leaf = plant.HasLeaves,
                LeafColor = plant.LeafColor,
                GrowthRate = plant.GrowthRate,
                Maintenance = plant.MaintenanceLevel,
                SaltTolerant = plant.IsSaltTolerant,
                CareLevel = plant.CareLevel
            };
        }

        // Mapeo de CreatePlantRequestDto a Plant (para crear nuevas plantas)
        public static Plant ToPlantFromCreateDto(this CreatePlantRequestDto createPlantRequest)
        {
            return new Plant
            {
                PlantName = createPlantRequest.plantName,
                ScientificName = createPlantRequest.ScientificName,
                Description = createPlantRequest.Description,
                GrowthCycle = createPlantRequest.GrowthCycle,
                WateringFrequency = createPlantRequest.Watering,
                HardinessZone = createPlantRequest.HardinessZone,
                HardinessZoneDescription = createPlantRequest.HardinessZoneDescription,
                FlowerDetails = createPlantRequest.Flowers,
                SunExposure = createPlantRequest.Sun,
                FruitDetails = createPlantRequest.Fruits,
                IsEdible = createPlantRequest.Edible,
                HasLeaves = createPlantRequest.Leaf,
                LeafColor = createPlantRequest.LeafColor,
                GrowthRate = createPlantRequest.GrowthRate,
                MaintenanceLevel = createPlantRequest.Maintenance,
                IsSaltTolerant = createPlantRequest.SaltTolerant,
                CareLevel = createPlantRequest.CareLevel
            };
        }

        // Mapeo de UpdatePlantRequestDto a Plant (para actualizar una planta existente)
        public static Plant ToPlantFromUpdateDto(this UpdatePlantRequestDto updatePlantRequest)
        {
            return new Plant
            {
                PlantName = updatePlantRequest.plantName,
                ScientificName = updatePlantRequest.ScientificName,
                Description = updatePlantRequest.Description,
                GrowthCycle = updatePlantRequest.GrowthCycle,
                WateringFrequency = updatePlantRequest.Watering,
                HardinessZone = updatePlantRequest.HardinessZone,
                HardinessZoneDescription = updatePlantRequest.HardinessZoneDescription,
                FlowerDetails = updatePlantRequest.Flowers,
                SunExposure = updatePlantRequest.Sun,
                FruitDetails = updatePlantRequest.Fruits,
                IsEdible = updatePlantRequest.Edible,
                HasLeaves = updatePlantRequest.Leaf,
                LeafColor = updatePlantRequest.LeafColor,
                GrowthRate = updatePlantRequest.GrowthRate,
                MaintenanceLevel = updatePlantRequest.Maintenance,
                IsSaltTolerant = updatePlantRequest.SaltTolerant,
                CareLevel = updatePlantRequest.CareLevel
            };
        }
    }
}
