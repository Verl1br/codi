using codi.Models;
using codi.DataBase;

namespace codi.Repository
{
    public class PostRepository : IPostRepository
    {
        ApplicationContext db;
        public PostRepository(ApplicationContext context)
        {
            db = context;
        }

        public List<Post> GetPosts(string userName)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == userName);

            if (user == null) throw new InvalidOperationException("User not found");

            var posts = db.Posts.Where(c => c.UserId == user.Id).ToList();

            return posts;
        }

        public List<Post> GetPostsForAllUsers()
        {
            var posts = db.Posts.ToList();

            return posts;
        }

        /*
        public Post GetPost(int id, string userName)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == userName);

            if (user == null) throw new InvalidOperationException("User not found");

            var post = db.Posts.FirstOrDefault(p => p.Id == id && p.UserId == user.Id);

            if (post == null) throw new InvalidOperationException("Post not found");

            return post;
        }*/

        public async Task<Post> Create(PostDto input)
        {
            var user = db.Users.FirstOrDefault(x => x.Id == input.UserId);

            if (input.Image == null) throw new InvalidOperationException("Image is null");

            if (user == null) throw new InvalidOperationException("User not found");

            Post newPost = new Post();

            newPost.Text = input.Text;
            newPost.UserId = input.UserId;
            newPost.User = user;
            newPost.ImageFileName = input.Image.FileName;

            db.Posts.Add(newPost);
            await db.SaveChangesAsync();
            return newPost;
        }

        public async Task<Post> Update(PostDto input, string userName)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == userName);

            if (user == null) throw new InvalidOperationException("User not found");

            var post = db.Posts.FirstOrDefault(p => p.UserId == user.Id && p.Id == input.Id);

            if (post == null) throw new InvalidOperationException("Post not found");

            post.Text = input.Text;
            db.Posts.Update(post);
            await db.SaveChangesAsync();
            return post;
        }

        public async Task<Post> Likes(int postId)
        {
            var post = db.Posts.FirstOrDefault(p => p.Id == postId);

            if (post == null) throw new InvalidOperationException("Post not found");

            post.Likes++;
            db.Posts.Update(post);
            await db.SaveChangesAsync();
            return post;
        }
    }
}