using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Custom;
using Api.Models;
using Api.Models.DTOs;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly UrbanGardeningContext _dbUrbanGardeningContext;
        private readonly Utils _utils;

        public AccesoController(UrbanGardeningContext dbUrbanGardeningContext, Utils utils)
        {
            _dbUrbanGardeningContext = dbUrbanGardeningContext;
            _utils = utils;
        }

        [HttpPost]
        [Route("Registrarse")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userDto)
        {
            var modelUser = new User
            {
                Name = userDto.Name,
                FirstName = userDto.FirstName,
                Surname = userDto.Surname,
                Age = userDto.Age,
                Email = userDto.Email,
                Password = _utils.EncriptarSHA256(userDto.Password),
                Telephone = userDto.Telephone,
                CreatedAt = DateTime.UtcNow
            };

            await _dbUrbanGardeningContext.Users.AddAsync(modelUser);
            await _dbUrbanGardeningContext.SaveChangesAsync();

            return modelUser.UserId != 0 
                ? StatusCode(StatusCodes.Status201Created, new { isSuccess = true })
                : StatusCode(StatusCodes.Status500InternalServerError, new { isSuccess = false });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var userFind = await _dbUrbanGardeningContext.Users
                .Where(u => u.Email == loginDto.Email &&
                             u.Password == _utils.EncriptarSHA256(loginDto.Password))
                .FirstOrDefaultAsync();

            if (userFind == null)
                return Unauthorized(new { isSuccess = false, token = "" });

            var token = _utils.GenerarJWT(userFind);
            return Ok(new {
    isSuccess = true,
    token,
    userId = userFind.UserId,
    name = userFind.Name,
    firstName = userFind.FirstName,
    surname = userFind.Surname,
    
});

        }
    }
}