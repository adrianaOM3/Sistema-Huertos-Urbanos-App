using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Garden
{
    public int GardenId { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? ImageUrl { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Comment>? Comments { get; set; }

}
