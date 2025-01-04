using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Body = comment.Body,
                Created = comment.Created,
                StockId = comment.StockId
            };
        }

        public static Comment ToComment(this CreateCommentDto createCommentDto)
        {
            return new Comment
            {
                Title = createCommentDto.Title,
                Body = createCommentDto.Body,
                Created = createCommentDto.Created,
                StockId = createCommentDto.StockId
            };
        }
    }
}