using codi.DataBase;
using codi.Models;

namespace codi.Repository
{
    public class CommentRepository : ICommentRepository
    {
        ApplicationContext db;

        public CommentRepository(ApplicationContext context)
        {
            db = context;
        }

        public async Task<Commentary> Create(CommentDto input)
        {
            var post = db.Posts.FirstOrDefault(x => x.Id == input.PostId);

            Commentary newComment = new Commentary();

            newComment.Text = input.Text;
            newComment.UserId = input.UserId;
            newComment.PostId = input.PostId;
            newComment.Post = post;

            db.Commentaries.Add(newComment);
            await db.SaveChangesAsync();

            return newComment;
        }

        public List<Commentary> GetComments(int postId)
        {
            var comments = db.Commentaries.Where(c => c.PostId == postId).ToList();

            return comments;
        }

        public async Task<Commentary> Likes(int commentId)
        {
            var commentary = db.Commentaries.FirstOrDefault(c => c.Id == commentId);

            if (commentary == null) throw new InvalidOperationException("Post not found");

            commentary.Likes++;
            db.Commentaries.Update(commentary);
            await db.SaveChangesAsync();
            return commentary;
        }
    }
}