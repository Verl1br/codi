
namespace codi.Models
{
    public class Commentary
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public int UserId { get; set; }

        public int Likes { get; set; }

        public int PostId { get; set; }

        public Post? Post { get; set; }
    }
}