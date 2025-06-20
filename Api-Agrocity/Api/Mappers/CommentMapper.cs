using Api.Dtos.Comment;
using Api.Models;

namespace Api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToDto(this Comment comment)
        {
            return new CommentDto
            {
                CommentId = comment.CommentId,
                UserId = comment.UserId,
                GardenId = comment.GardenId,
                Description = comment.Description,
                CreatedAt = comment.CreatedAt
            };
        }

        public static Comment ToCommentFromCreateDto(this CreateCommentRequestDto commentDto)
        {
            return new Comment
            {
               
                UserId = commentDto.UserId,
                GardenId = commentDto.GardenId,
                Description = commentDto.Description,
                CreatedAt = commentDto.CreatedAt
            };
        }
        public static Comment ToCommentFromUpdateDto(this UpdateCommentRequestDto commentDto)
        {
            return new Comment
            {
                CommentId = commentDto.CommentId,
                UserId = commentDto.UserId,
                GardenId = commentDto.GardenId,
                Description = commentDto.Description,
                CreatedAt = commentDto.CreatedAt
            };
        }


       public static CommentByPublicationDto ToCommentByPublicationDto(this Comment comment)
        {
            return new CommentByPublicationDto
            {
                CommentId = comment.CommentId,
                UserName = comment.User?.Name,
                Description = comment.Description
            };
        }
    }
}