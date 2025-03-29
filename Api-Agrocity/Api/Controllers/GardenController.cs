using Api.Models;
using Api.Models.DTOs.Garden;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Api.Mappers;

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
            var gardens = await _context.Gardens.ToListAsync();
            var gardensDto = gardens.Select(gardens => gardens.ToDto());
            return Ok(gardensDto);
        }

        //Endpoint Get By Gardens
        [HttpGet("{gardenId}")]
        public async Task<IActionResult> getByIdGardens([FromRoute] int gardenId)
        {
            var _garden = await _context.Gardens.FirstOrDefaultAsync(gar => gar.GardenId == gardenId);
            if (_garden == null)
            {
                return NotFound();
            }
            return Ok(_garden.ToDto());
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
            var gardenModel = await _context.Gardens.FirstOrDefaultAsync(_garden => _garden.GardenId == gardenId);
            if (gardenModel == null)
            {
                return NotFound();
            }
    
            gardenModel.UserId = gardenDto.UserId;
            gardenModel.Name = gardenDto.Name;
            gardenModel.Description = gardenDto.Description;
            gardenModel.CreatedAt = gardenDto.CreatedAt;

            await _context.SaveChangesAsync();

            return Ok(gardenModel.ToDto());
        }

        //Endpoint delete an Gardens
        [HttpDelete]
        [Route("{gardenId}")]
        public async Task<IActionResult> DeleteGarden([FromRoute] int gardenId)
        {
            var gardenModel = await _context.Gardens.FirstOrDefaultAsync(_garden => _garden.GardenId == gardenId);
            if (gardenModel == null)
            {
                return NotFound();
            }
            _context.Gardens.Remove(gardenModel);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}