using Api.Models;
using Api.Models.DTOs.Garden;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Api.Mappers;
using Api.Models.DTOs;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class GardenController : ControllerBase
    {
        private readonly UrbanGardeningContext _context;
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedImages");

        public GardenController(UrbanGardeningContext context)
        {
            _context = context;
        }

        //Endpoint Get All Gardens
        [HttpGet]
        public async Task<IActionResult> GetAllGarden()
        {
            var gardens = await _context.Gardens
            .Include(g => g.User)
            .ToListAsync();
            var gardensDto = gardens.Select(gardens => gardens.ToDto());
            return Ok(gardensDto);
        }

        //Endpoint Get By Gardens
        [HttpGet("{gardenId}")]
        public async Task<IActionResult> getByIdGardens([FromRoute] int gardenId)
        {



            var garden = await _context.Gardens
             .Include(g => g.User)
            .FirstOrDefaultAsync(g => g.GardenId == gardenId);

            if (garden == null)
            {
                return NotFound(new
                {
                    message = "Jardín no encontrado."
                });
            }
            return Ok(new
            {
                message = "Jardín encontrado.",
                data = garden.ToDto()
            });



        }

        //Endpoint new create Gardens

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateGardenRequestDto gardenDto)
        {
            try
            {
                if (gardenDto.File == null || gardenDto.File.Length == 0)
                    return BadRequest("No file uploaded.");

                var gardenModel = gardenDto.ToGardenFromCreateDto();


                await _context.Gardens.AddAsync(gardenModel);
                await _context.SaveChangesAsync();

                var fileName = gardenModel.GardenId.ToString() + Path.GetExtension(gardenDto.File.FileName);
                var filePath = Path.Combine(_imagePath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await gardenDto.File.CopyToAsync(stream);
                }

                gardenModel.ImageUrl = fileName;

                _context.Gardens.Update(gardenModel);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(getByIdGardens), new { gardenId = gardenModel.GardenId }, gardenModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }

        /*[HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGardenRequestDto createGardenRequestDto)
        {
            var gardenModel = createGardenRequestDto.ToGardenFromCreateDto();
            await _context.Gardens.AddAsync(gardenModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(getByIdGardens), new { gardenId = gardenModel.GardenId }, gardenModel.ToDto());
        }*/

        //Endpoint update Gardens

        [HttpPut("{gardenId}")]
        public async Task<IActionResult> Update(int gardenId, [FromForm] UpdateGardenRequestDto gardenDto)
        {
            try
            {
                var existingGarden = await _context.Gardens.FindAsync(gardenId);
                if (existingGarden == null)
                    return NotFound(new { message = $"No se encontró un jardín con ID {gardenId}." });

                var userExists = await _context.Users.AnyAsync(u => u.UserId == gardenDto.UserId);
                if (!userExists)
                    return BadRequest(new { message = $"No se encontró un usuario con ID {gardenDto.UserId}." });

                existingGarden.UserId = gardenDto.UserId;
                existingGarden.Name = gardenDto.Name;
                existingGarden.Description = gardenDto.Description;
                existingGarden.CreatedAt = gardenDto.CreatedAt;

                // Procesar nueva imagen si viene una
                if (gardenDto.File != null && gardenDto.File.Length > 0)
                {
                    // Generar un nombre único para la nueva imagen
                    var uniqueFileName = $"{existingGarden.GardenId}_{Guid.NewGuid()}{Path.GetExtension(gardenDto.File.FileName)}";
                    var newFilePath = Path.Combine(_imagePath, uniqueFileName);

                    // Guardar la nueva imagen en disco
                    using (var stream = new FileStream(newFilePath, FileMode.Create))
                    {
                        await gardenDto.File.CopyToAsync(stream);
                    }

                    // Eliminar imagen anterior si existe
                    if (!string.IsNullOrEmpty(existingGarden.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_imagePath, existingGarden.ImageUrl);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Asignar nuevo nombre de imagen
                    existingGarden.ImageUrl = uniqueFileName;
                }

                _context.Gardens.Update(existingGarden);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Jardín actualizado correctamente.",
                    garden = existingGarden
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error interno al actualizar el jardín: {ex.Message}" });
            }
        }




        //Endpoint delete an Gardens
        [HttpDelete]
        [Route("{gardenId}")]
        public async Task<IActionResult> DeleteGarden([FromRoute] int gardenId)
        {
            var gardenModel = await _context.Gardens.FirstOrDefaultAsync(_garden => _garden.GardenId == gardenId);


            if (gardenModel == null)
            {
                return NotFound(new
                {
                    message = "Jardín no encontrado para eliminar."
                });
            }

            _context.Gardens.Remove(gardenModel);

            await _context.SaveChangesAsync();

            return NoContent();


        }

        [HttpGet("garden/{gardenId}/user")]
        public async Task<IActionResult> GetUserByGardenId([FromRoute] int gardenId)
        {
            var garden = await _context.Gardens
                .Include(g => g.User)
                .FirstOrDefaultAsync(g => g.GardenId == gardenId);

            if (garden == null)
            {
                return NotFound(new { message = "Jardín no encontrado." });
            }

            if (garden.User == null)
            {
                return NotFound(new { message = "Este jardín no tiene un usuario asignado." });
            }

            return Ok(new
            {
                message = "Jardín y usuario encontrados correctamente.",
                garden = new
                {
                    garden.GardenId,
                    garden.Name,
                    garden.Description,
                    garden.CreatedAt
                },
                user = new
                {
                    garden.User.UserId,
                    garden.User.Name,
                    garden.User.FirstName,
                    garden.User.Email
                }
            });
        }

        //enpoint de los usuarios relacionados con un jardin
        /* [HttpGet("garden/{userId}")]
         public IActionResult GetGardensByUser(int userId)
         {

             var gardens = _context.Gardens
                 .Where(g => g.UserId == userId)
                 .Include(g => g.User)
                 .ToList();

             if (gardens.Any())
             {

                 var gardenDtos = gardens.Select(g => g.ToDto()).ToList();

                 return Ok(new
                 {
                     message = "Jardines encontrados",
                     data = gardenDtos
                 });
             }
             else
             {
                 return NotFound(new
                 {
                     message = "No se encontraron jardines para el usuario especificado."
                 });
             }
         }*/
        [HttpGet("garden/{userId}")]
        public IActionResult GetGardensByUser(int userId)
        {
            var gardens = _context.Gardens
                .Where(g => g.UserId == userId)
                .Include(g => g.User)
                .ToList();

            if (gardens.Any())
            {
                var gardenDtos = gardens.Select(g => g.ToDto()).ToList();
                return Ok(gardenDtos);
            }
            else
            {
                return NotFound("No se encontraron jardines para el usuario especificado.");
            }
        }




    }





}



