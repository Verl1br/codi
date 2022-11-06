using codi.Models;

namespace codi.Services
{
    public interface ICommentService
    {
        Task<Commentary> Create(CommentDto input);

        List<Commentary> GetComments(int postId);

        Task<Commentary> Likes(int commentId);
    }
}