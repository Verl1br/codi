
namespace codi.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public string ImageFileName { get; set; } = string.Empty;

        public int Likes { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }

        public List<Commentary>? Commentaries { get; set; }

    }
}