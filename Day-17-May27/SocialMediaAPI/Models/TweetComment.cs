using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaAPI.Models
{
    public class TweetComment
    {
        [Key]
        public int CommentId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int TweetId { get; set; }
        public DateTime CommentedAt { get; set; } = DateTime.Now;

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("TweetId")]
        public Tweet? Tweet { get; set; }
    }
}