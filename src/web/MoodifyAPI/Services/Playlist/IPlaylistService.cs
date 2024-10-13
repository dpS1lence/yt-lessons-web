namespace MoodifyAPI.Services.Playlist
{
    public interface IPlaylistService
    {
        List<string> GetPlaylistByMood(string moodOrActivity);
    }
}
