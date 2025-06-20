namespace Api.Dtos.Comment
{
    public class CommentDto
    {
        public int CommentId { get; set; }

        public int? UserId { get; set; }

        public string? UserName { get; set; } 

        public int? GardenId { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

    }
}
