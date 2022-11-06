using codi.Repository;
using codi.Models;

namespace codi.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repo;

        public CommentService(ICommentRepository repo)
        {
            this.repo = repo;
        }

        public Task<Commentary> Create(CommentDto input)
        {
            return repo.Create(input);
        }

        public List<Commentary> GetComments(int postId)
        {
            return repo.GetComments(postId);
        }

        public Task<Commentary> Likes(int commentId)
        {
            return repo.Likes(commentId);
        }

        /*
        public Post GetPost(int id, string userName)
        {
            return repo.GetPost(id, userName);
        }*/
    }
}