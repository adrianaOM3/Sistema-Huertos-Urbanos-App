using Api.Dtos.Reminder;
using Api.Mappers;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/reminders")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly UrbanGardeningContext _context;

        public ReminderController(UrbanGardeningContext context)
        {
            _context = context;
        }

        // Obtener todos los recordatorios
       [HttpGet]
public async Task<IActionResult> GetAll()
{
    var reminders = await _context.Reminders
                                  .Include(r => r.Plant)  // Incluir la relación Plant
                                  .ToListAsync();

    var remindersDto = reminders.Select(reminder => ReminderMapper.ToReminderDto(reminder));
    return Ok(remindersDto);
}

        // Obtener los recordatorios por UserId
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId([FromRoute] int userId)
        {
            var reminders = await _context.Reminders
                 .Include(r => r.Plant) 
                .Where(r => r.UserId == userId)
                .ToListAsync();
            
            if (reminders == null || !reminders.Any())
            {
                return NotFound("No reminders found for this user.");
            }

            var remindersDto = reminders.Select(reminder => ReminderMapper.ToReminderDto(reminder));
            return Ok(remindersDto);
        }

        // Crear un nuevo recordatorio
        [HttpPost]
public async Task<IActionResult> Create([FromBody] CreateReminderRequestDto reminderDto)
{
    if (reminderDto.UserId == null || reminderDto.PlantId == null || reminderDto.ReminderDate == null)
    {
        return BadRequest("All fields are required.");
    }

    // Mapear el DTO al modelo de la base de datos
    var reminder = ReminderMapper.ToReminderFromCreateDto(reminderDto);
   
    // Agregar el nuevo recordatorio al contexto y guardar los cambios
    _context.Reminders.Add(reminder);
    await _context.SaveChangesAsync();

    // Mapear la respuesta
    var reminderDtoResponse = ReminderMapper.ToReminderDto(reminder);
    return CreatedAtAction(nameof(GetAll), new { id = reminder.ReminderId }, reminderDtoResponse);
}

[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Buscar el recordatorio por su ID
            var reminder = await _context.Reminders.FindAsync(id);

            // Verificar si el recordatorio existe
            if (reminder == null)
            {
                return NotFound("Reminder not found.");
            }

            // Eliminar el recordatorio
            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();

            return NoContent();  // Retorna un estado 204 indicando que la eliminación fue exitosa
        }
    }

    }

