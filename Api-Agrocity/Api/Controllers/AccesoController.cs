using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api.Custom;
using Api.Models;
using Api.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.Mail;

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
            return Ok(new
            {
                isSuccess = true,
                token,
                userId = userFind.UserId,
                name = userFind.Name,
                firstName = userFind.FirstName,
                surname = userFind.Surname,

            });

        }
        
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDTO model)
        {
            var user = await _dbUrbanGardeningContext.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user == null)
                return NotFound(new { message = "User not found" });

            // Generar token
            var token = Guid.NewGuid().ToString();
            user.PasswordResetToken = token;
            user.ResetTokenExpires = DateTime.UtcNow.AddHours(1);

            await _dbUrbanGardeningContext.SaveChangesAsync();

            var resetLink = $"{Request.Scheme}://{Request.Host}/reset-password?token={token}";
            var emailBody = $"<p>Click <a href=\"{resetLink}\">here</a> to reset your password. The link expires in 1 hour.</p>";

            await _utils.SendEmailAsync(user.Email, "Password Reset", emailBody);

            return Ok(new { message = "Email sent successfully" });
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDTO model)
        {
            var user = await _dbUrbanGardeningContext.Users
                .FirstOrDefaultAsync(u => u.PasswordResetToken == model.Token && u.ResetTokenExpires > DateTime.UtcNow);

            if (user == null)
                return BadRequest(new { message = "Invalid or expired token" });

            user.Password = _utils.EncriptarSHA256(model.NewPassword);
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _dbUrbanGardeningContext.SaveChangesAsync();

            return Ok(new { message = "Password reset successful" });
        }
    }
}