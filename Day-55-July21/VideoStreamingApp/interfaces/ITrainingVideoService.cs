
using VideoStreamingApp.Dtos;
using VideoStreamingApp.models;

namespace VideoStreamingApp.interfaces
{
    public interface ITrainingVideoService
    {
        Task<TrainingVideo> UploadVideo(UploadVideoDto dto);
        Task<List<TrainingVideo>> GetAllVideos();
        Task<Stream?> GetVideoStream(int id);
    }
}