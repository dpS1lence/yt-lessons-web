
namespace MoodifyAPI.Services.Playlist
{
    public class PlaylistService : IPlaylistService
    {
        public List<string> GetPlaylistByMood(string moodOrActivity)
        {
            var playlists = new Dictionary<string, List<string>>
            {
                { "бягане", new List<string> { "Eye of the Tiger", "Can't Stop - Red Hot Chili Peppers", "Run Boy Run" } },
                { "релакс", new List<string> { "Weightless - Marconi Union", "River Flows in You - Yiruma", "Clair de Lune - Debussy" } },
                { "фокус", new List<string> { "Lo-fi Beats", "Instrumental Jazz", "Focus - Apple Music" } }
            };

            if (playlists.ContainsKey(moodOrActivity.ToLower()))
            {
                return playlists[moodOrActivity.ToLower()];
            }

            return new List<string> { "Няма налични плейлисти за това настроение." };
        }
    }
}
