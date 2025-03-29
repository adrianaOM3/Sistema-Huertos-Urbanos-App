using Api.Models;
using Api.Models.DTOs;
using Api.Custom;

namespace Api.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                FirstName = user.FirstName,
                Surname = user.Surname,
                Age = user.Age,
                Telephone = user.Telephone,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };
        }

        public static User ToUserFromCreateDto(CreateUserRequestDto dto, Utils utils)
        {
            return new User
            {
                Name = dto.Name,
                FirstName = dto.FirstName,
                Surname = dto.Surname,
                Age = dto.Age,
                Telephone = dto.Telephone,
                Email = dto.Email,
                Password = utils.EncriptarSHA256(dto.Password), // Encriptar la contraseña
                CreatedAt = DateTime.UtcNow
            };
        }

        public static User ToUserFromUpdateDto(UpdateUserRequestDto dto, Utils utils)
        {
            return new User
            {
                Name = dto.Name,
                FirstName = dto.FirstName,
                Surname = dto.Surname,
                Age = dto.Age,
                Telephone = dto.Telephone,
                Email = dto.Email,
                Password = utils.EncriptarSHA256(dto.Password) // Encriptar la contraseña
            };
        }
    }
}
