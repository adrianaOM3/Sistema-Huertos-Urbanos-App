using System;

namespace Api.Dtos.Plant
{
    public class CreatePlantRequestDto
    {
        public string plantName { get; set; }
        public string ScientificName { get; set; }
        public string Description { get; set; }
        public string GrowthCycle { get; set; }
        public string Watering { get; set; }
        public string HardinessZone { get; set; }
        public string HardinessZoneDescription { get; set; }
        public string Flowers { get; set; }
        public string Sun { get; set; }
        public string Fruits { get; set; }
        public bool Edible { get; set; }
        public bool Leaf { get; set; }
        public string LeafColor { get; set; }
        public string GrowthRate { get; set; }
        public string Maintenance { get; set; }
        public bool SaltTolerant { get; set; }
        public string CareLevel { get; set; }
    }
}
