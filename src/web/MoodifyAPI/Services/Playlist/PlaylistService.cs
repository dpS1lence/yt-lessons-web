
using MongoDB.Driver;
using MoodifyAPI.Contracts.Helpers;
using PlaylistModel = MoodifyAPI.Models.Playlist;
using OpenAI.Chat;
using System.Text;
using MoodifyAPI.Models;

namespace MoodifyAPI.Services.Playlist
{
    public class PlaylistService(ChatClient client, IMongoClient mongoClient) : IPlaylistService
    {
        public async Task GetPlaylistByMood(string moodOrActivity)
        {
            ArgumentNullException.ThrowIfNull(nameof(moodOrActivity));

            var systemMessage = GeneratePrompt.SystemMessage();
            var userMessage = GeneratePrompt.UserMessage(moodOrActivity);

            var messages = new List<ChatMessage>
            {
                new SystemChatMessage(systemMessage),
                new UserChatMessage(userMessage)
            };

            var response = await client.CompleteChatAsync(messages);
            var responseText = response.Value.Content[0].Text;

            var lines = responseText
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            var database = mongoClient.GetDatabase("moodify-v1");
            var playlistsCollection = database.GetCollection<PlaylistModel>("playlists");
            var songsCollection = database.GetCollection<Song>("songs");

            var playlist = new PlaylistModel { Name = moodOrActivity };
            var uniqueSongs = new HashSet<Song>();

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2) continue;

                string songName = parts[0].Trim();
                string authorName = parts[1].Trim();

                var song = new Song { Name = songName, Author = authorName };

                var existingSong = await songsCollection.Find(s => s.Name == songName).FirstOrDefaultAsync();
                if (existingSong != null)
                {
                    uniqueSongs.Add(existingSong);
                }
                else
                {
                    await songsCollection.InsertOneAsync(song);
                    uniqueSongs.Add(song);
                }
            }

            playlist.Songs = uniqueSongs;
            await playlistsCollection.InsertOneAsync(playlist);
        }
    }
}
