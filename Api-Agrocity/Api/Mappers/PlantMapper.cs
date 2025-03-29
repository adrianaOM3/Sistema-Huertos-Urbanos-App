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
                ScientificName = plant.ScientificName,
                Description = plant.Description,
                GrowthCycle = plant.GrowthCycle,
                WateringFrequency = plant.WateringFrequency,
                HardinessZone = plant.HardinessZone,
                HardinessZoneDescription = plant.HardinessZoneDescription,
                FlowerDetails = plant.FlowerDetails,
                SunExposure = plant.SunExposure,
                FruitDetails = plant.FruitDetails,
                IsEdible = plant.IsEdible ?? false,
                HasLeaves = plant.HasLeaves?? false,
                LeafColor = plant.LeafColor,
                GrowthRate = plant.GrowthRate,
                MaintenanceLevel = plant.MaintenanceLevel,
                IsSaltTolerant = plant.IsSaltTolerant?? false,
                CareLevel = plant.CareLevel
            };
        }

        // Mapeo de CreatePlantRequestDto a Plant (para crear nuevas plantas)
        public static Plant ToPlantFromCreateDto(this CreatePlantRequestDto createPlantRequest)
        {
            return new Plant
            {
                PlantName = createPlantRequest.PlantName,
                ScientificName = createPlantRequest.ScientificName,
                Description = createPlantRequest.Description,
                GrowthCycle = createPlantRequest.GrowthCycle,
                WateringFrequency = createPlantRequest.WateringFrequency,
                HardinessZone = createPlantRequest.HardinessZone,
                HardinessZoneDescription = createPlantRequest.HardinessZoneDescription,
                FlowerDetails = createPlantRequest.FlowerDetails,
                SunExposure = createPlantRequest.SunExposure,
                FruitDetails = createPlantRequest.FruitDetails,
                IsEdible = createPlantRequest.IsEdible,
                HasLeaves = createPlantRequest.HasLeaves,
                LeafColor = createPlantRequest.LeafColor,
                GrowthRate = createPlantRequest.GrowthRate,
                MaintenanceLevel = createPlantRequest.MaintenanceLevel,
                IsSaltTolerant = createPlantRequest.IsSaltTolerant,
                CareLevel = createPlantRequest.CareLevel
            };
        }

        // Mapeo de UpdatePlantRequestDto a Plant (para actualizar una planta existente)
        public static Plant ToPlantFromUpdateDto(this UpdatePlantRequestDto updatePlantRequest)
        {
            return new Plant
            {
                PlantName = updatePlantRequest.PlantName,
                ScientificName = updatePlantRequest.ScientificName,
                Description = updatePlantRequest.Description,
                GrowthCycle = updatePlantRequest.GrowthCycle,
                WateringFrequency = updatePlantRequest.WateringFrequency,
                HardinessZone = updatePlantRequest.HardinessZone,
                HardinessZoneDescription = updatePlantRequest.HardinessZoneDescription,
                FlowerDetails = updatePlantRequest.FlowerDetails,
                SunExposure = updatePlantRequest.SunExposure,
                FruitDetails = updatePlantRequest.FruitDetails,
                IsEdible = updatePlantRequest.IsEdible,
                HasLeaves = updatePlantRequest.HasLeaves,
                LeafColor = updatePlantRequest.LeafColor,
                GrowthRate = updatePlantRequest.GrowthRate,
                MaintenanceLevel = updatePlantRequest.MaintenanceLevel,
                IsSaltTolerant = updatePlantRequest.IsSaltTolerant,
                CareLevel = updatePlantRequest.CareLevel
            };
        }
    }
}
