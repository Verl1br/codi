using codi.Models;

namespace codi.Repository
{
    public interface ICommentRepository
    {
        Task<Commentary> Create(CommentDto input);

        List<Commentary> GetComments(int postId);

        Task<Commentary> Likes(int commentId);

        //Commentary GetCommentaries(int id, string userName);
    }
}