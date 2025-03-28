namespace Api.Models.DTOs
{
    public class UserRegisterDTO
    {
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Telephone { get; set; }
        
    }
}