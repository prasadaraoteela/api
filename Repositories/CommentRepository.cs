using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;

namespace api.Repositories
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        public async Task<Comment> CreateAsync(Comment comment)
        {
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }
            context.Comments.Remove(comment);
            await context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }
            return comment;
        }


        public async Task<Comment?> UpdateAsync(int id, UpdateCommentDto updateCommentDto)
        {
            var existingComment = await context.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = updateCommentDto.Title;
            existingComment.Body = updateCommentDto.Body;
            existingComment.Created = updateCommentDto.Created;
            existingComment.StockId = updateCommentDto.StockId;

            await context.SaveChangesAsync();
            return existingComment;
        }
    }
}