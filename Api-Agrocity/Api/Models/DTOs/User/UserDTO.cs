namespace Api.Models.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public string? Telephone { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
    }

    public class CreateUserRequestDto
    {
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public string? Telephone { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class UpdateUserRequestDto
    {
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public string? Telephone { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
