using Microsoft.EntityFrameworkCore;
using VideoStreamingApp.models;

namespace VideoStreamingApp.contexts
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<TrainingVideo> TrainingVideos { get; set; }
    }
}