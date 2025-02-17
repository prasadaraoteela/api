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

        public static Comment ToComment(this CreateCommentDto createCommentDto, int stockId)
        {
            return new Comment
            {
                Title = createCommentDto.Title,
                Body = createCommentDto.Body,
                Created = DateTime.Now,
                StockId = stockId
            };
        }

        public static Comment ToComment(this UpdateCommentDto updateCommentDto)
        {
            return new Comment
            {
                Title = updateCommentDto.Title,
                Body = updateCommentDto.Body,
            };
        }
    }
}