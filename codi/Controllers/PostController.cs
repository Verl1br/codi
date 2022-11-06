using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using codi.Services;
using codi.Models;


namespace codi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost("post")]
        public async Task<IActionResult> CreatePost([FromForm] PostDto request)
        {
            var post = await postService.Create(request);
            return Ok(post);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdatePost(PostDto request)
        {
            if (User.Identity == null) return BadRequest();

            var userName = User.Identity.Name;

            if (userName == null) return BadRequest();

            var post = await postService.Update(request, userName);
            return Ok(post);
        }

        [HttpGet("get")]
        public IActionResult GetPosts()
        {
            if (User.Identity == null) return BadRequest();

            var userName = User.Identity.Name;

            if (userName == null) return BadRequest();

            var posts = postService.GetPosts(userName);
            return Ok(posts);
        }

        [HttpGet("getforall"), AllowAnonymous]
        public IActionResult GetPostsForAllUsers()
        {
            var posts = postService.GetPostsForAllUsers();
            return Ok(posts);
        }

        [HttpPut("likes/{id:int}")]
        public async Task<IActionResult> Likes(int id)
        {
            var post = await postService.Likes(id);
            return Ok(post);
        }

        /* 
        [HttpGet("getpost/{id:int}")]
        public IActionResult GetPost(int id)
        {
            var userName = User.Identity.Name;

            if (userName == null) return BadRequest();

            var posts = postService.GetPost(id, userName);
            return Ok(posts);
        }*/
    }
}