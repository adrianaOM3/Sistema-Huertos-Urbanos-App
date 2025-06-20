using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Publication
{
    public int PublicationId { get; set; }

    public int? UserId { get; set; }

    public string? Title { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? Likes { get; set; }

    public virtual User? User { get; set; }
}
