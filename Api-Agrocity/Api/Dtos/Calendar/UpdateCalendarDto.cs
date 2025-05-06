using System;
using System.Collections.Generic;

namespace Api.Dtos.Calendar
{
    public class UpdateCalendarDto
    {
        public DateTime? CalendarDate { get; set; }

        public string? Description { get; set; }
    }
}