using System;
using System.Collections.Generic;

namespace Api.Dtos.Comment
{
    public class UpdateCommentRequestDto
    {
        public int? UserId { get; set; }

        public int? GardenId { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }
}
}