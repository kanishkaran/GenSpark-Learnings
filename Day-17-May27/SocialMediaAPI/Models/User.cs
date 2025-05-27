using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SocialMediaAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Tweet>? tweets;
        public ICollection<TweetLike>? tweetLikes;
        public ICollection<TweetComment>? tweetComments;
        public ICollection<TweetHashtag>? tweetHashtags;
        public ICollection<UserFollow>? followers;
        public ICollection<UserFollow>? following;
    }
}