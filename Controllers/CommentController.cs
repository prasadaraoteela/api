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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await commentRepository.GetAllAsync();
            return Ok(comments.Select(comment => comment.ToCommentDto()).ToList());
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<CommentDto>> GetCommentById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound("Comment does not exist");
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<ActionResult<CommentDto>> CreateComment([FromRoute] int stockId, [FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await stockRepository.StockExists(stockId))
            {
                return BadRequest("Stock does not exist.");
            }
            var comment = await commentRepository.CreateAsync(createCommentDto.ToComment(stockId));
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<CommentDto>> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await commentRepository.UpdateAsync(id, updateCommentDto.ToComment());
            if (comment == null)
            {
                return NotFound("Comment does not exist.");
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<CommentDto>> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comment = await commentRepository.DeleteAsync(id);
            if (comment == null)
            {
                return NotFound("Comment does not exist.");
            }
            return Ok(comment.ToCommentDto());
        }

    }
}