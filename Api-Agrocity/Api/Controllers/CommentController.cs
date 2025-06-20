using Api.Dtos.Comment;
using Api.Mappers; // Asegúrate de tener el CommentMapper
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/comments")]
    
    public class CommentController : ControllerBase
    {
        private readonly UrbanGardeningContext _context;

        public CommentController(UrbanGardeningContext context)
        {
            _context = context;
        }

        // Obtener todos los comentarios
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _context.Comments.ToListAsync();
            var commentsDto = comments.Select(comment => CommentMapper.ToDto(comment));
            return Ok(commentsDto);
        }

        // Obtener un comentario por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(CommentMapper.ToDto(comment));
        }

        // Crear un nuevo comentario
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCommentRequestDto commentDto)
        {
            var commentModel = CommentMapper.ToCommentFromCreateDto(commentDto);
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = commentModel.CommentId }, CommentMapper.ToDto(commentModel));
        }

        // Actualizar un comentario existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto commentDto)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == id);
            if (commentModel == null)
            {
                return NotFound();
            }

            var updatedComment = CommentMapper.ToCommentFromUpdateDto(commentDto);

            commentModel.Description = updatedComment.Description;
    

            await _context.SaveChangesAsync();

            return Ok(CommentMapper.ToDto(commentModel));
        }

        // Eliminar un comentario
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(c => c.CommentId == id);
            if (commentModel == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

      [HttpGet("garden/{gardenId}")]
public async Task<IActionResult> GetByGardenId([FromRoute] int gardenId)
{
    var comments = await _context.Comments
        .Include(c => c.User)
        .Where(c => c.GardenId == gardenId)
        .ToListAsync();

    var commentsDto = comments.Select(comment => comment.ToCommentByPublicationDto());
    return Ok(commentsDto);
}

    }
}
