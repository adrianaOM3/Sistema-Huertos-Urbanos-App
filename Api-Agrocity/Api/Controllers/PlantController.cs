using Api.Models;
using Api.Dtos.Plant;
using Api.Mappers; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace Api.Controllers
{
    [Route("api/plants")]
    //[Authorize]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly UrbanGardeningContext _context;

        public PlantController(UrbanGardeningContext context)
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
            var plant = await _context.Plants.FirstOrDefaultAsync(p => p.PlantId == id);
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
            return CreatedAtAction(nameof(GetById), new { id = plantModel.PlantId }, PlantMapper.ToDto(plantModel)); // Usar el mapper para convertir a DTO
        }

        // Actualizar una planta existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePlantRequestDto plantDto)
        {
            var plantModel = await _context.Plants.FirstOrDefaultAsync(p => p.PlantId == id);
            if (plantModel == null)
            {
                return NotFound();
            }

            // Usar el mapper para actualizar los valores
            var updatedPlant = PlantMapper.ToPlantFromUpdateDto(plantDto);
            plantModel.PlantName = updatedPlant.PlantName;
            plantModel.Description = updatedPlant.Description;
            plantModel.ImageUrl = updatedPlant.ImageUrl;
            await _context.SaveChangesAsync();

            return Ok(PlantMapper.ToDto(plantModel)); // Usar el mapper para convertir a DTO
        }

        // Eliminar una planta
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var plantModel = await _context.Plants.FirstOrDefaultAsync(p => p.PlantId == id);
            if (plantModel == null)
            {
                return NotFound();
            }

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

            for (int page = 1; page <= 3; page++)
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
                            // Traducción del nombre común y descripción
                            var translatedName = await TranslateToSpanish(basicPlant.CommonName ?? "Planta sin nombre");
                            var translatedDescription = await TranslateToSpanish(detailData.Description ?? "");

                            resultPlants.Add(new PlantDto
                            {
                                PlantId = basicPlant.Id,
                                PlantName = translatedName,
                                Description = translatedDescription,
                                ImageUrl = basicPlant.DefaultImage?.RegularUrl
                            });
                        }
                    }
                    catch
                    {
                        continue; // ignoramos errores por planta
                    }
                }
            }

            return Ok(resultPlants);
        }


        private async Task<string> TranslateToSpanish(string englishText)
        {
            if (string.IsNullOrWhiteSpace(englishText))
                return "Sin descripción disponible";

            using var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("q", englishText),
                new KeyValuePair<string, string>("source", "en"),
                new KeyValuePair<string, string>("target", "es"),
                new KeyValuePair<string, string>("format", "text")
            });

            try
            {
                var response = await client.PostAsync("https://libretranslate.com/translate", content);
                if (!response.IsSuccessStatusCode)
                    return englishText; // fallback

                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<Models.LibreTranslateResponse>(json);
                return result?.TranslatedText ?? englishText;
            }
            catch
            {
                return englishText;
            }
        }



    }
}
