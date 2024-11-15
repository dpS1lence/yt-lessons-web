using System.Text;

namespace MoodifyAPI.Contracts.Helpers
{
    public static class GeneratePrompt
    {
        public static string SystemMessage()
        {
            return "You are a music recommendation assistant. " +
                "When asked for a playlist, respond only with the names of 8 " +
                "songs and their authors that match the user's specified mood or state. " +
                "Keep responses brief and consistent: provide exactly 8 songs and their respective " +
                "authors in the format 'Song Name - Author Name' on separate lines. " +
                "An example response should look like this: " +
                "\"Song 1 - Artist 1\r\nSong 2 - Artist 2\r\nSong 3 - Artist 3\r\n...\r\nSong 8 - Artist 8\"";
        }

        public static string UserMessage(string mood)
        {
            var requestBuilder = new StringBuilder();

            requestBuilder.Append($"Please recommend 8 songs that match the mood: {mood}.");

            return requestBuilder.ToString();
        }
    }
}
