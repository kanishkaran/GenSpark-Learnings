using Microsoft.AspNetCore.Mvc;
using VideoStreamingApp.Dtos;
using VideoStreamingApp.interfaces;

namespace VideoStreamingApp.controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly ITrainingVideoService _trainingVideoService;

        public VideoController(ITrainingVideoService videoService)
        {
            _trainingVideoService = videoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var videos = await _trainingVideoService.GetAllVideos();
            return Ok(videos);
            }
           catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/stream")]
        public async Task<IActionResult> Stream(int id)
        {
            try
            {
                var videoStream = await _trainingVideoService.GetVideoStream(id);
                return File(videoStream, "video/mp4");
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] UploadVideoDto uploadVideo)
        {
            try
            {
                var video = await _trainingVideoService.UploadVideo(uploadVideo);
                return Ok(video);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}