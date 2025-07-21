
using Azure.Storage.Blobs;
using VideoStreamingApp.Dtos;
using VideoStreamingApp.interfaces;
using VideoStreamingApp.models;

namespace VideoStreamingApp.services
{
    public class TrainingVideoService : ITrainingVideoService
    {
        private readonly ITrainingVideoRepository _repo;
        private readonly BlobContainerClient _blobClient;

        public TrainingVideoService(ITrainingVideoRepository repository,
                                    BlobContainerClient blobContainer)
        {
            _repo = repository;
            _blobClient = blobContainer;
        }
        public async Task<List<TrainingVideo>> GetAllVideos()
        {
            var all = await _repo.GetAllAsync();
            return all.ToList();
        }

        public async Task<Stream?> GetVideoStream(int id)
        {
            var video = await _repo.GetByIdAsync(id);

            var blobClient = new BlobClient(new Uri(video.BlobUrl));

            var response = await blobClient.DownloadStreamingAsync();
            return response.Value.Content;
            
        }

        public async Task<TrainingVideo> UploadVideo(UploadVideoDto dto)
        {
            if (dto.Video == null || dto.Title == null)
            {
                throw new Exception("Invalid Data. Please provide valid data");
            }

            var blobName = $"{Guid.NewGuid()}_{dto?.Video?.FileName}";
            var blobClient = _blobClient.GetBlobClient(blobName);

            using var stream = dto?.Video?.OpenReadStream();

            await blobClient.UploadAsync(stream);

            var video = new TrainingVideo
            {
                Title = dto.Title,
                Description = dto.Description,
                BlobUrl = blobClient.Uri.ToString(),
                UploadDate = DateTime.UtcNow
            };

            await _repo.AddAsync(video);
            return video;
        }
    }
}