namespace codi.Models
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public IFormFile? Image { get; set; }

        public int UserId { get; set; }
    }
}