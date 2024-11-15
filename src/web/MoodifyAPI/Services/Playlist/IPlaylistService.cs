namespace MoodifyAPI.Services.Playlist
{
    public interface IPlaylistService
    {
        Task<string> GetPlaylistByMood(string moodOrActivity);
    }
}
