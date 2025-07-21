

using Microsoft.EntityFrameworkCore;
using VideoStreamingApp.contexts;
using VideoStreamingApp.interfaces;
using VideoStreamingApp.models;

namespace VideoStreamingApp.repositories
{
    public class TrainingVideoRepository : ITrainingVideoRepository
    {
        private readonly AppDbContext _context;

        public TrainingVideoRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<TrainingVideo> AddAsync(TrainingVideo video)
        {
            await _context.AddAsync(video);
            await _context.SaveChangesAsync();
            return video;
        }

        public async Task<IEnumerable<TrainingVideo>> GetAllAsync()
        {
            var videos = await _context.TrainingVideos.OrderByDescending(v => v.UploadDate).ToListAsync() ?? throw new Exception("No entries found in the database");
            return videos;
        }

        public async Task<TrainingVideo> GetByIdAsync(int Id)
        {
            var video = await _context.TrainingVideos.FindAsync(Id) ?? throw new Exception($"Video with id {Id} not found");
            return video;

        }
    }
}