
using System.ComponentModel.DataAnnotations.Schema;


namespace SocialMediaAPI.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public ICollection<TweetLike>? likes;

        public ICollection<TweetComment>? comments;

        public ICollection<TweetHashtag>? hashtags;
    }
}