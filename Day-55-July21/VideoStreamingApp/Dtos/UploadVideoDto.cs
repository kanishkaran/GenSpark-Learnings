

namespace VideoStreamingApp.Dtos
{
    public class UploadVideoDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? Video { get; set; }
    }
}