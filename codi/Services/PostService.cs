using codi.Repository;
using codi.Models;

namespace codi.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repo;

        public PostService(IPostRepository repo)
        {
            this.repo = repo;
        }

        public Task<Post> Create(PostDto input)
        {
            if (input.Image == null) throw new InvalidOperationException("Image is null");

            string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(input.Image.FileName);

            string SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", ImageName);

            using (var stream = new FileStream(SavePath, FileMode.Create))
            {
                input.Image.CopyTo(stream);
            }

            return repo.Create(input);
        }

        public List<Post> GetPosts(string userName)
        {
            return repo.GetPosts(userName);
        }

        public List<Post> GetPostsForAllUsers()
        {
            return repo.GetPostsForAllUsers();
        }

        public Task<Post> Update(PostDto input, string userName)
        {
            return repo.Update(input, userName);
        }

        public Task<Post> Likes(int postId)
        {
            return repo.Likes(postId);
        }
        /*
        public Post GetPost(int id, string userName)
        {
            return repo.GetPost(id, userName);
        }*/
    }
}