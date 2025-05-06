using Api.Models;
using Api.Dtos.Calendar;    
using Api.Mappers;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;        
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarController : ControllerBase
    {
        private readonly UrbanGardeningContext _context;

        public CalendarController(UrbanGardeningContext context)
        {
            _context = context;
        }

        // GET: api/calendar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarDto>>> GetCalendars()
        {
            var calendars = await _context.Calendars.ToListAsync();
            return Ok(calendars.Select(c => c.ToDto()));
        }
    
        // GET: api/calendar/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarDto>> GetCalendar(int id)
        {
            var calendar = await _context.Calendars.FindAsync(id);

            if (calendar == null)
            {
                return NotFound();
            }

            return Ok(calendar.ToDto());
        }

        // POST: api/calendar
        [HttpPost]
        public async Task<ActionResult<CalendarDto>> CreateCalendar([FromBody] CreateCalendarDto createCalendarDto)
        {
            var calendar = new Calendar
            {
                CalendarDate = createCalendarDto.CalendarDate,
                Description = createCalendarDto.Description
            };

            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCalendar), new { id = calendar.CalendarId }, calendar.ToDto());
        }

        // PUT: api/calendar/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCalendar(int id, [FromBody] CreateCalendarDto updateDto)
        {
            var calendar = await _context.Calendars.FindAsync(id);

            if (calendar == null)
            {
                return NotFound();
            }

            calendar.CalendarDate = updateDto.CalendarDate;
            calendar.Description = updateDto.Description;

            _context.Entry(calendar).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/calendar/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendar(int id)
        {
            var calendar = await _context.Calendars.FindAsync(id);

            if (calendar == null)
            {
                return NotFound();
            }

            _context.Calendars.Remove(calendar);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
