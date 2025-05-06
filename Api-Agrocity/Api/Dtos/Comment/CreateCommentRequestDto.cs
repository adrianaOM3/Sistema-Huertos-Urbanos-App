using System;
using System.Collections.Generic;

namespace Api.Dtos.Comment
{
    public class CreateCommentRequestDto
    {

        public int? UserId { get; set; }

        public int? PublicationId { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }


    }
}