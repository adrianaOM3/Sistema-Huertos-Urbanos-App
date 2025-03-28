using api.Models;
using api.Dtos.Plant;
using api.Mappers; // Asegúrate de incluir la clase PlantMapper
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using api.Data;

namespace api.Controllers
{
    [Route("api/plants")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PlantController(ApplicationDBContext context)
        {
            _context = context;
        }

        // Obtener todas las plantas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plants = await _context.Plants.ToListAsync();
            var plantsDto = plants.Select(plant => PlantMapper.ToDto(plant)); // Usar el mapper para convertir a DTO
            return Ok(plantsDto);
        }

        // Obtener una planta por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);
            if (plant == null)
            {
                return NotFound();
            }
            return Ok(PlantMapper.ToDto(plant)); // Usar el mapper para convertir a DTO
        }

        // Crear una nueva planta
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlantRequestDto plantDto)
        {
            var plantModel = PlantMapper.ToPlantFromCreateDto(plantDto); // Usar el mapper para convertir el DTO a la entidad
            await _context.Plants.AddAsync(plantModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = plantModel.Id }, PlantMapper.ToDto(plantModel)); // Usar el mapper para convertir a DTO
        }

        // Actualizar una planta existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePlantRequestDto plantDto)
        {
            var plantModel = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);
            if (plantModel == null)
            {
                return NotFound();
            }

            // Usar el mapper para actualizar los valores
            var updatedPlant = PlantMapper.ToPlantFromUpdateDto(plantDto);
            plantModel.Name = updatedPlant.Name;
            plantModel.ScientificName = updatedPlant.ScientificName;
            plantModel.Description = updatedPlant.Description;
            plantModel.GrowthCycle = updatedPlant.GrowthCycle;
            plantModel.Watering = updatedPlant.Watering;
            plantModel.HardinessZone = updatedPlant.HardinessZone;
            plantModel.HardinessZoneDescription = updatedPlant.HardinessZoneDescription;
            plantModel.Flowers = updatedPlant.Flowers;
            plantModel.Sun = updatedPlant.Sun;
            plantModel.Fruits = updatedPlant.Fruits;
            plantModel.Edible = updatedPlant.Edible;
            plantModel.Leaf = updatedPlant.Leaf;
            plantModel.LeafColor = updatedPlant.LeafColor;
            plantModel.GrowthRate = updatedPlant.GrowthRate;
            plantModel.Maintenance = updatedPlant.Maintenance;
            plantModel.SaltTolerant = updatedPlant.SaltTolerant;
            plantModel.CareLevel = updatedPlant.CareLevel;

            await _context.SaveChangesAsync();

            return Ok(PlantMapper.ToDto(plantModel)); // Usar el mapper para convertir a DTO
        }

        // Eliminar una planta
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var plantModel = await _context.Plants.FirstOrDefaultAsync(p => p.Id == id);
            if (plantModel == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plantModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
