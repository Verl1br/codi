
namespace codi.Models
{
    public class CommentDto
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public int UserId { get; set; }

        public int PostId { get; set; }
    }
}