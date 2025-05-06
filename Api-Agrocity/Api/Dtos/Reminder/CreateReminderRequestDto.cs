using System;
using System.Collections.Generic;

namespace Api.Dtos.Reminder
{
    public class CreateReminderRequestDto
    {
        public int? UserId { get; set; }

        public int? PlantId { get; set; }

        public String? ReminderDate { get; set; }

        public string? TypeReminder { get; set; }
    }
}