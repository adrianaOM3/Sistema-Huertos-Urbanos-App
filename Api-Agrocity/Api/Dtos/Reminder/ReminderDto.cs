namespace Api.Dtos.Reminder
{
    public class ReminderDto
    {
        public int ReminderId { get; set; }

        public int? UserId { get; set; }

        public int? PlantId { get; set; }

        public DateOnly? ReminderDate { get; set; }

        public string? TypeReminder { get; set; }

        public string? PlantName { get; set; } 
}
}