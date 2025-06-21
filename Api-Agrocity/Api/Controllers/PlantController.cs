using Api.Models;
using Api.Dtos.Plant;
using Api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("api/plants")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly UrbanGardeningContext _context;

        public PlantController(UrbanGardeningContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plants = await _context.Plants.ToListAsync();
            var plantsDto = plants.Select(plant => PlantMapper.ToDto(plant));
            return Ok(plantsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.PlantId == id);
            if (plant == null)
                return NotFound();

            return Ok(PlantMapper.ToDto(plant));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlantRequestDto plantDto)
        {
            var plantModel = PlantMapper.ToPlantFromCreateDto(plantDto);
            await _context.Plants.AddAsync(plantModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = plantModel.PlantId }, PlantMapper.ToDto(plantModel));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePlantRequestDto plantDto)
        {
            var plantModel = await _context.Plants.FirstOrDefaultAsync(p => p.PlantId == id);
            if (plantModel == null)
                return NotFound();

            var updatedPlant = PlantMapper.ToPlantFromUpdateDto(plantDto);
            plantModel.PlantName = updatedPlant.PlantName;
            plantModel.Description = updatedPlant.Description;
            plantModel.ImageUrl = updatedPlant.ImageUrl;

            await _context.SaveChangesAsync();
            return Ok(PlantMapper.ToDto(plantModel));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var plantModel = await _context.Plants.FirstOrDefaultAsync(p => p.PlantId == id);
            if (plantModel == null)
                return NotFound();

            _context.Plants.Remove(plantModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("external")]
        public async Task<IActionResult> GetExternalPlants()
        {
            var httpClient = new HttpClient();
            var resultPlants = new List<PlantDto>();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            for (int page = 1; page <= 2; page++)
            {
                var response = await httpClient.GetAsync($"https://perenual.com/api/v2/species-list?key=sk-i8ue68564c324ae8711104&page={page}");

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"Error al obtener plantas en la página {page}");

                var json = await response.Content.ReadAsStringAsync();
                var wrapper = JsonSerializer.Deserialize<ExternalPlantWrapper>(json, options);

                if (wrapper?.data == null || !wrapper.data.Any())
                    break;

                foreach (var basicPlant in wrapper.data)
                {
                    try
                    {
                        var detailResponse = await httpClient.GetAsync($"https://perenual.com/api/v2/species/details/{basicPlant.Id}?key=sk-i8ue68564c324ae8711104");
                        if (!detailResponse.IsSuccessStatusCode)
                            continue;

                        var detailJson = await detailResponse.Content.ReadAsStringAsync();
                        var detailData = JsonSerializer.Deserialize<ExternalPlantDto>(detailJson, options);

                        if (detailData != null)
                        {
                            resultPlants.Add(new PlantDto
                            {
                                PlantId = basicPlant.Id,
                                PlantName = basicPlant.CommonName ?? "Planta sin nombre",
                                Description = detailData.Description ?? "Sin descripción disponible",
                                ImageUrl = basicPlant.DefaultImage?.RegularUrl
                            });
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return Ok(resultPlants);
        }
    }
}
