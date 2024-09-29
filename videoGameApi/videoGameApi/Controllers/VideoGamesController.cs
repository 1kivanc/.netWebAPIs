using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using videoGameApi.Models;
using videoGameApi.Repositories;

namespace videoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGamesController : ControllerBase
    {
        private readonly IVideoGameRepository _videoGameRepository;

        public VideoGamesController(IVideoGameRepository videoGameRepository)
        {
            _videoGameRepository = videoGameRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetAllAsync()
        {
            var videoGames = await _videoGameRepository.GetAllAsync();
            return Ok(videoGames);
        }

        [HttpGet("{id}",Name="GetById")]
        public async Task<ActionResult<VideoGame>> GetByIdAsync(int id)
        {
            var videoGame = await _videoGameRepository.GetByIdAsync(id);
            if (videoGame == null)
            {
                return NotFound();
            }
            return Ok(videoGame);
        }
        [HttpPost]
        public async Task<ActionResult<VideoGame>> AddAsync(VideoGame videoGame)
        {
            await _videoGameRepository.AddSync(videoGame);
            return CreatedAtAction("GetById", new { id = videoGame.Id }, videoGame);
        }

        [HttpPut]
        public async Task<ActionResult<VideoGame>> UpdateAsync(int id,VideoGame videoGame)
        {
            var existingVideoGame = await _videoGameRepository.GetByIdAsync(id);
            if (existingVideoGame == null)
            {
                return NotFound("Video game not found");
            }

            videoGame.Id = id;
            await _videoGameRepository.UpdateAsync(videoGame);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var existingVideoGame = await _videoGameRepository.GetByIdAsync(id);
            if (existingVideoGame == null)
            {
                return NotFound("Video game not found");
            }

            await _videoGameRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
