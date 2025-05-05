using Api.Dtos.Calendar;
using Api.Models;

namespace Api.Mappers
{
    public static class CalendarMapper
    {
        public static CalendarDto  ToDto(this Calendar calendar)
        {
            

            return new CalendarDto
            {
                CalendarId = calendar.CalendarId,
                CalendarDate = calendar.CalendarDate,
                Description = calendar.Description
            };
        }
        public static Calendar ToCalendarFromCreateDto(this CalendarDto calendarDto)
        {
           
            return new Calendar
            {
                CalendarId = calendarDto.CalendarId,
                CalendarDate = calendarDto.CalendarDate,
                Description = calendarDto.Description
            };
        }
        public static Calendar ToCalendarFromUpdateDto(this CalendarDto calendarDto)
        {
            
            return new Calendar
            {
                CalendarId = calendarDto.CalendarId,
                CalendarDate = calendarDto.CalendarDate,
                Description = calendarDto.Description
            };
        }
    }
}   