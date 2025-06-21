using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Pest;
using Api.Mappers;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PestController : ControllerBase
    {
        private readonly UrbanGardeningContext _context;
        private readonly HttpClient _httpClient;

        public PestController(UrbanGardeningContext context)
        {
            _context = context;
            _httpClient = new HttpClient();

        }

        // GET: api/pest
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pests = await _context.Pests.ToListAsync();
            var pestsDto = pests.Select(p => p.ToPestDto());
            return Ok(pestsDto);
        }

        // GET: api/pest/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid pest ID");

            var pest = await _context.Pests.FindAsync(id);

            if (pest == null)
                return NotFound();

            return Ok(pest.ToPestDto());
        }

        // POST: api/pest
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePestRequestDto pestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(pestDto.CommonName))
                return BadRequest("Common name is required");

            if (await _context.Pests.AnyAsync(p => p.CommonName == pestDto.CommonName))
                return Conflict("Pest with this name already exists");

            var pest = pestDto.ToPestFromCreateDto();

            await _context.Pests.AddAsync(pest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = pest.PestId }, pest.ToPestDto());
        }

        // PUT: api/pest/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromRoute] int id,
            [FromBody] UpdatePestRequestDto pestDto)
        {
            if (id <= 0)
                return BadRequest("Invalid pest ID");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pest = await _context.Pests.FindAsync(id);

            if (pest == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(pestDto.CommonName) &&
                pest.CommonName != pestDto.CommonName &&
                await _context.Pests.AnyAsync(p => p.CommonName == pestDto.CommonName))
            {
                return Conflict("Pest with this name already exists");
            }

            pest.CommonName = pestDto.CommonName ?? pest.CommonName;
            pest.ScientificName = pestDto.ScientificName ?? pest.ScientificName;
            pest.Description = pestDto.Description ?? pest.Description;
            pest.Solution = pestDto.Solution ?? pest.Solution;
            pest.Host = pestDto.Host ?? pest.Host;
            pest.ImageUrl = pestDto.ImageUrl ?? pest.ImageUrl;

            _context.Pests.Update(pest);
            await _context.SaveChangesAsync();

            return Ok(pest.ToPestDto());
        }

        // GET: api/pest/byplant/5
        /*[HttpGet("byplant/{plantId}")]
        public async Task<IActionResult> GetByPlantId([FromRoute] int plantId)
        {
            if (plantId <= 0)
                return BadRequest("No se encontraron plantas con ese identificador.");

            var plant = await _context.Plants
                .Include(p => p.Pests)
                .FirstOrDefaultAsync(p => p.PlantId == plantId);

            if (plant == null)
                return NotFound($"No plant found with ID {plantId}.");

            if (plant.Pests == null || !plant.Pests.Any())
                return Ok(new List<PestDto>()); // Empty list, no pests related

            var pestDtos = plant.Pests.Select(p => p.ToPestDto());

            return Ok(pestDtos);
        }*/

        // GET: api/pest/search
        [HttpGet("external")]
        public async Task<IActionResult> GetExternalPests()
        {
            var httpClient = new HttpClient();
            var allPests = new List<ExternalPestDto>();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            for (int page = 1; page <= 1; page++)
            {
                var response = await httpClient.GetAsync($"https://perenual.com/api/pest-disease-list?key=sk-NoVy681ef04e0e60c10347&page={page}");

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, $"Failed to fetch pests on page {page}");
                }

                var json = await response.Content.ReadAsStringAsync();
                var wrapper = JsonSerializer.Deserialize<ExternalPestWrapper>(json, options);

                if (wrapper?.data == null || !wrapper.data.Any())
                {
                    break; // Ya no hay más datos
                }

                allPests.AddRange(wrapper.data);
            }

            var pestDtos = allPests.Select(p => p.ToPestDto()).ToList();

            return Ok(pestDtos);
        }




        // DELETE: api/pest/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0)
                return BadRequest("Invalid pest ID");

            var pest = await _context.Pests.FindAsync(id);

            if (pest == null)
                return NotFound();

            _context.Pests.Remove(pest);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }


}