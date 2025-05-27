
using System.ComponentModel.DataAnnotations.Schema;


namespace SocialMediaAPI.Models
{
    public class TweetLike
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TweetId { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("TweetId")]
        public Tweet? Tweet { get; set; }

    }
}