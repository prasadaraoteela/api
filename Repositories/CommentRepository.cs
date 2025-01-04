using Microsoft.EntityFrameworkCore;

using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repositories
{
    public class CommentRepository(ApplicationDBContext context) : ICommentRepository
    {
        public async Task<List<Comment>> GetAllAsync()
        {
            return await context.Comments.ToListAsync();
        }
    }
}