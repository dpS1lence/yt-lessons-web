namespace MoodifyAPI.Services.Playlist
{
    public interface IPlaylistService
    {
        Task GetPlaylistByMood(string moodOrActivity);
    }
}
