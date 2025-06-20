namespace Api.Dtos.Comment
{
    public class CommentByPublicationDto
    {
        public int CommentId { get; set; } 
        public string? UserName { get; set; }
        public string? Description { get; set; }

        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
    }
}