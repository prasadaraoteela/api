using Microsoft.AspNetCore.Mvc;

using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController(ICommentRepository commentRepository) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetComments()
        {
            var comments = await commentRepository.GetAllAsync();
            return comments.Select(comment => comment.ToCommentDto()).ToList();
        }

    }
}