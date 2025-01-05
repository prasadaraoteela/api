using Microsoft.AspNetCore.Mvc;

using api.Dtos.Comment;
using api.Interfaces;
using api.Mappers;

namespace api.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController(ICommentRepository commentRepository, IStockRepository stockRepository) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<CommentDto>>> GetComments()
        {
            var comments = await commentRepository.GetAllAsync();
            return comments.Select(comment => comment.ToCommentDto()).ToList();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentById([FromRoute] int id)
        {
            var comment = await commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return comment.ToCommentDto();
        }

        [HttpPost("{stockId}")]
        public async Task<ActionResult<CommentDto>> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentDto createCommentDto)
        {
            if (!await stockRepository.StockExists(stockId))
            {
                return BadRequest("Stock does not exist.");
            }
            var comment = await commentRepository.CreateAsync(createCommentDto.ToComment(stockId));
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment.ToCommentDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<CommentDto>> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            var comment = await commentRepository.UpdateAsync(id, updateCommentDto);
            if (comment == null)
            {
                return NotFound();
            }
            return comment.ToCommentDto();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<CommentDto>> DeleteComment([FromRoute] int id)
        {
            var comment = await commentRepository.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return comment.ToCommentDto();
        }

    }
}