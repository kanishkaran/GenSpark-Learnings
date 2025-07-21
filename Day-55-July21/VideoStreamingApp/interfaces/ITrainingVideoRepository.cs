
using VideoStreamingApp.models;

namespace VideoStreamingApp.interfaces
{
    public interface ITrainingVideoRepository
    {
        Task<IEnumerable<TrainingVideo>> GetAllAsync();
        Task<TrainingVideo> GetByIdAsync(int Id);
        Task<TrainingVideo> AddAsync(TrainingVideo video);
    }
}