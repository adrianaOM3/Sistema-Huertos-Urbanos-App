using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? FirstName { get; set; }

    public string? Surname { get; set; }

    public int? Age { get; set; }

    public string? Telephone { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? ResetTokenExpires { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Garden> Gardens { get; set; } = new List<Garden>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();
}
