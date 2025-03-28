using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Plant
    {
        public int plantId { get; set; }
        public string plantName { get; set; }
        public string ScientificName { get; set; }
        public string Description { get; set; }
        public string GrowthCycle { get; set; }
        public string wateringFrequency { get; set; }
        public string HardinessZone { get; set; }
        public string HardinessZoneDescription { get; set; }
        public string flowerDetails { get; set; }
        public string sunExposure { get; set; }
        public string fruitDetails { get; set; }
        public bool isEdible { get; set; }
        public bool hasLeaves { get; set; }
        public string LeafColor { get; set; }
        public string GrowthRate { get; set; }
        public string maintenanceLevel { get; set; }
        public bool isSaltTolerant { get; set; }
        public string CareLevel { get; set; }
    }
}
