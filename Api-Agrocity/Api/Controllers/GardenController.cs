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
        public async Task<IActionResult> Create([FromBody] CreateGardenRequestDto createGardenRequestDto)
        {
            var gardenModel = createGardenRequestDto.ToGardenFromCreateDto();
            await _context.Gardens.AddAsync(gardenModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(getByIdGardens), new { gardenId = gardenModel.GardenId }, gardenModel.ToDto());
        }

        //Endpoint update Gardens
        [HttpPut]
        [Route("{gardenId}")]
        public async Task<IActionResult> UpdateGarden([FromRoute] int gardenId, [FromBody] UpdateGardenRequestDto gardenDto)
        {
            var gardenModel = await _context.Gardens
             .Include(g => g.User)
            .FirstOrDefaultAsync(_garden => _garden.GardenId == gardenId);

            if (gardenModel == null)
            {
                return NotFound(new
                {
                    message = "Jardín no encontrado para actualizar."
                });
            }

            gardenModel.UserId = gardenDto.UserId;
            gardenModel.Name = gardenDto.Name;
            gardenModel.Description = gardenDto.Description;
            gardenModel.CreatedAt = gardenDto.CreatedAt;

            await _context.SaveChangesAsync();



            return Ok(new
            {
                message = "Jardín actualizado correctamente.",
                data = gardenModel.ToDto()
            });


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
        }



    }





}



