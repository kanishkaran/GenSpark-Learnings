using System.ComponentModel.DataAnnotations;


namespace VideoStreamingApp.models
{
    public class TrainingVideo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
        [Required]
        public string BlobUrl { get; set; } = string.Empty;
    }
}