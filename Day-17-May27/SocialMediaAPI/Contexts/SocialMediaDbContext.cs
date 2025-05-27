using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Models;

namespace SocialMediaAPI.Contexts
{
    public class SocialMediaDbContext : DbContext
    {
        public SocialMediaDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> users { get; set; }
        public DbSet<Tweet> tweets { get; set; }
        public DbSet<TweetLike> tweetLikes { get; set; }
        public DbSet<TweetComment> tweetComments { get; set; }
        public DbSet<Hashtag> hashtags { get; set; }
        public DbSet<TweetHashtag> tweetHashtags { get; set; }
        public DbSet<UserFollow> userFollows { get; set; }

    }
}