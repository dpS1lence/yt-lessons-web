using Microsoft.AspNetCore.Mvc;
using MoodifyAPI.Services.Playlist;

namespace MoodifyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController(IPlaylistService playlistService) : Controller
    {
        [HttpGet("GetPlaylistByMood")]
        public async Task<IActionResult> GetPlaylistByMood(string mood)
        {
            await playlistService.GetPlaylistByMood(mood);

            return Ok();
        }
    }
}
