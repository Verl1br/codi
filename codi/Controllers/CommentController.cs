using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using codi.Services;
using codi.Models;


namespace codi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost("post")]
        public async Task<IActionResult> CreatePost(CommentDto request)
        {
            var post = await commentService.Create(request);
            return Ok(post);
        }

        [HttpGet("getcomments/{id:int}")]
        public IActionResult GetPost(int id)
        {
            var posts = commentService.GetComments(id);
            return Ok(posts);
        }

        [HttpPut("likes/{id:int}")]
        public async Task<IActionResult> Likes(int id)
        {
            var post = await commentService.Likes(id);
            return Ok(post);
        }
    }
}