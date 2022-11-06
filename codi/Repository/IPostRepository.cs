using codi.Models;

namespace codi.Repository
{
    public interface IPostRepository
    {
        Task<Post> Create(PostDto input);

        List<Post> GetPosts(string userName);

        List<Post> GetPostsForAllUsers();

        Task<Post> Update(PostDto input, string userName);

        Task<Post> Likes(int postId);

        //Post GetPost(int id, string userName);
    }
}