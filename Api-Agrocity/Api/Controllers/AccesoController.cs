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
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDTO model)
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

        [HttpGet("/reset-password")]
        [HttpPost("/reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordForm([FromQuery] string token, [FromForm] string? newPassword)
        {
            if (Request.Method == HttpMethods.Post)
            {
                // Validar token y cambiar contraseña
                if (string.IsNullOrEmpty(newPassword))
                    return Content(BuildHtml("Por favor ingresa una nueva contraseña", false), "text/html");

                var user = await _dbUrbanGardeningContext.Users
                    .FirstOrDefaultAsync(u => u.PasswordResetToken == token && u.ResetTokenExpires > DateTime.UtcNow);

                if (user == null)
                    return Content(BuildHtml("Token inválido o expirado. Solicita uno nuevo.", false), "text/html");

                user.Password = _utils.EncriptarSHA256(newPassword);
                user.PasswordResetToken = null;
                user.ResetTokenExpires = null;
                await _dbUrbanGardeningContext.SaveChangesAsync();

                return Content(BuildHtml("¡Tu contraseña ha sido cambiada exitosamente!", true), "text/html");
            }

            // Si es GET, mostrar formulario
            return Content(BuildHtml(token: token), "text/html");
        }

        private string BuildHtml(string? message = null, bool success = false, string? token = null)
        {
            string baseStyle = @"
                body {
                    font-family: 'Segoe UI', sans-serif;
                    background-color: #ffffff;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    height: 100vh;
                    margin: 0;
                }
                .container {
                    background-color: #ffffff;
                    border-radius: 12px;
                    padding: 32px;
                    max-width: 400px;
                    width: 90%;
                    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
                    text-align: center;
                }
                h2 {
                    color: #4CAF50;
                    margin-bottom: 24px;
                }
                label {
                    display: block;
                    margin-bottom: 8px;
                    font-weight: 600;
                    color: #333;
                    text-align: left;
                }
                input[type='password'], input[type='submit'] {
                    width: 100%;
                    padding: 12px;
                    margin: 10px 0;
                    border-radius: 8px;
                    font-size: 16px;
                }
                input[type='password'] {
                    border: 1px solid #ccc;
                }
                input[type='submit'] {
                    background-color: #4CAF50;
                    color: white;
                    border: none;
                    cursor: pointer;
                    transition: background-color 0.3s ease;
                }
                input[type='submit']:hover {
                    background-color: #43A047;
                }
                .message {
                    padding: 16px;
                    margin-top: 16px;
                    border-radius: 8px;
                    font-weight: 500;
                }
                .success {
                    background-color: #e8f5e9;
                    color: #2e7d32;
                    border: 1px solid #81c784;
                }
                .error {
                    background-color: #ffebee;
                    color: #c62828;
                    border: 1px solid #ef5350;
                }";

            string formHtml = token != null ? $@"
                <form method='post' action='/reset-password?token={token}'>
                    <input type='hidden' name='token' value='{token}' />
                    <label>Nueva Contraseña:</label>
                    <input type='password' name='newPassword' required />
                    <input type='submit' value='Cambiar Contraseña' />
                </form>" : "";

            string messageHtml = message != null
                ? $"<div class='message {(success ? "success" : "error")}'>{message}</div>"
                : "";

            return $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <title>Restablecer Contraseña</title>
                    <style>{baseStyle}</style>
                </head>
                <body>
                    <div class='container'>
                        <h2>Restablecer Contraseña</h2>
                        {messageHtml}
                        {formHtml}
                    </div>
                </body>
                </html>";
        }
    }
}