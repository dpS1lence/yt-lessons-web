using Microsoft.AspNetCore.Mvc;
using MoodifyAPI.Services.Playlist;

namespace MoodifyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController(IPlaylistService playlistService) : Controller
    {
        [HttpGet("GetPlaylistByMood")]
        public IActionResult GetPlaylistByMood(string mood)
        {
            var playlist = playlistService.GetPlaylistByMood(mood);

            return Ok(playlist);
        }
    }
}
