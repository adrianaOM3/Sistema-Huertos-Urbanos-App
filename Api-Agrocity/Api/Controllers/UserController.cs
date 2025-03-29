using Api.Models;
using Api.Mappers;
using Api.Models.DTOs;
using Api.Custom;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/users")]
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UrbanGardeningContext _context;
        private readonly Utils _utils;

        public UserController(UrbanGardeningContext context, Utils utils)
        {
            _context = context;
            _utils = utils;
        }

        // Obtener todos los usuarios
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            var usersDto = users.Select(user => UserMapper.ToDto(user));
            return Ok(usersDto);
        }

        // Obtener un usuario por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(UserMapper.ToDto(user));
        }

        // Crear un nuevo usuario
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequestDto userDto)
        {
            var userModel = UserMapper.ToUserFromCreateDto(userDto, _utils);
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = userModel.UserId }, UserMapper.ToDto(userModel));
        }

        // Actualizar un usuario existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserRequestDto userDto)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (userModel == null)
            {
                return NotFound();
            }

            var updatedUser = UserMapper.ToUserFromUpdateDto(userDto, _utils);
            userModel.Name = updatedUser.Name;
            userModel.FirstName = updatedUser.FirstName;
            userModel.Surname = updatedUser.Surname;
            userModel.Age = updatedUser.Age;
            userModel.Telephone = updatedUser.Telephone;
            userModel.Email = updatedUser.Email;
            userModel.Password = updatedUser.Password;

            await _context.SaveChangesAsync();
            return Ok(UserMapper.ToDto(userModel));
        }

        // Eliminar un usuario
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (userModel == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
