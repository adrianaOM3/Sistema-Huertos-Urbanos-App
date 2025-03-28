namespace Api.Models;

namespace api.Models
public partial class Plant
{
    public int PlantId { get; set; }

    public string PlantName { get; set; } = null!;

    public string? ScientificName { get; set; }

    public string? Description { get; set; }

    public string? GrowthCycle { get; set; }

    public string? WateringFrequency { get; set; }

    public string? HardinessZone { get; set; }

    public string? HardinessZoneDescription { get; set; }

    public string? FlowerDetails { get; set; }

    public string? SunExposure { get; set; }

    public string? FruitDetails { get; set; }

    public bool? IsEdible { get; set; }

    public bool? HasLeaves { get; set; }

    public string? LeafColor { get; set; }

    public string? GrowthRate { get; set; }

    public string? MaintenanceLevel { get; set; }

    public bool? IsSaltTolerant { get; set; }

    public string? CareLevel { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastModified { get; set; }

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    public virtual ICollection<Pest> Pests { get; set; } = new List<Pest>();
}
