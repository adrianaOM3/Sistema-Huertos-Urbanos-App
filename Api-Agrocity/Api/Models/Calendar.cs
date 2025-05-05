using System;
using System.Collections.Generic;

namespace Api.Models;

public partial class Calendar
{
    public int CalendarId { get; set; }

    public DateTime? CalendarDate { get; set; }

    public string? Description { get; set; }
}
