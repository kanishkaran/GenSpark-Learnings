using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaAPI.Models
{
    public class TweetHashtag
    {
        [Key]
        public int TweetId { get; set; }
        public int HashtagId { get; set; }

        [ForeignKey("TweetId")]
        public Tweet? Tweet { get; set; }

        [ForeignKey("HashtagId")]
        public Hashtag? Hashtag { get; set; }
    }
}