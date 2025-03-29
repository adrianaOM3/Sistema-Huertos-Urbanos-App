using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto.Pest;
using Api.Mappers;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PestController : ControllerBase
    {
        private readonly UrbanGardeningContext _context;

        public PestController(UrbanGardeningContext context)
        {
            _context = context;
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