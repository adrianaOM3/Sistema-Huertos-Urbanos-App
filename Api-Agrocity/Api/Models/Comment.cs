using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? UserId { get; set; }

    public int? GardenId { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Garden? Garden { get; set; }

    public virtual User? User { get; set; }
}
