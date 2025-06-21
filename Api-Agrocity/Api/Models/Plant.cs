using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Plant
{
    public int PlantId { get; set; }

    public string PlantName { get; set; } = null!;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    public virtual ICollection<Garden> PlantGardens { get; set; } = new List<Garden>();
}