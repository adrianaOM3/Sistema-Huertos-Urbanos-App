using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Reminder
{
    public int ReminderId { get; set; }

    public int? UserId { get; set; }

    public int? PlantId { get; set; }

    public DateOnly? ReminderDate { get; set; }

    public string? TypeReminder { get; set; }

    public virtual Plant? Plant { get; set; }

    public virtual User? User { get; set; }
}
