using Api.Dtos.Reminder;
using Api.Models;

namespace Api.Mappers
{
    public static class ReminderMapper
    {
        // Mapeo para la respuesta
        public static ReminderDto ToReminderDto(this Reminder reminder)
        {
            return new ReminderDto
            {
                ReminderId = reminder.ReminderId,
                UserId = reminder.UserId,
                PlantId = reminder.PlantId,
                ReminderDate = reminder.ReminderDate,
                TypeReminder = reminder.TypeReminder,
                PlantName = reminder.Plant?.PlantName
            };
        }

        // Mapeo para crear el recordatorio
        public static Reminder ToReminderFromCreateDto(this CreateReminderRequestDto reminderDto)
        {
            // Convertir la fecha de string a DateOnly
            DateOnly? reminderDate = reminderDto.ReminderDate != null ? DateOnly.Parse(reminderDto.ReminderDate) : null;

            return new Reminder
            {
                UserId = reminderDto.UserId,
                PlantId = reminderDto.PlantId,
                ReminderDate = reminderDate,
                TypeReminder = reminderDto.TypeReminder
            };
        }
    }
}
