
using MoodifyAPI.Contracts.Helpers;
using OpenAI.Chat;
using System.Text;

namespace MoodifyAPI.Services.Playlist
{
    public class PlaylistService(ChatClient client) : IPlaylistService
    {
        public async Task<string> GetPlaylistByMood(string moodOrActivity)
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

            var playlist = new Dictionary<string, string>();

            var lines = responseText.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var parts = line.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    var songName = parts[0].Trim();
                    var artistName = parts[1].Trim();

                    playlist[songName] = artistName;
                }
            }

            var result = new StringBuilder();
            foreach (var entry in playlist)
            {
                result.AppendLine($"{entry.Key} - {entry.Value}");
            }

            return result.ToString();
        }
    }
}
