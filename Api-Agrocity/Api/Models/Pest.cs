using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Pest
{
    public int PestId { get; set; }

    public string? CommonName { get; set; }

    public string? ScientificName { get; set; }

    public string? Description { get; set; }

    public string? Solution { get; set; }

    public string? Host { get; set; }

    public string? ImageUrl { get; set; }

    public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();

}
