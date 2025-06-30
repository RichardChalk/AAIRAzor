using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenAI.Chat;

namespace AAIRAzor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AzureOpenAIClient _client;
        public List<string> Suggestions { get; private set; }

        public IndexModel(AzureOpenAIClient client) => _client = client;

        public void OnGet()
        {
            var chat = _client.GetChatClient("Systementor-o4-mini");
            var msgs = new List<ChatMessage>()
            {
                new SystemChatMessage("You are a helpful assistant."),
                new UserChatMessage("I am going to York, England, what 3 things should I see?")
            };
            var result = chat.CompleteChat(msgs);

            // 1. Först delar koden upp den mottagna texten i en sträng för
            // varje rad genom Split('\n', StringSplitOptions.RemoveEmptyEntries),
            // vilket tar bort tomma rader.
            // 2. Sedan går Select(s => s.Trim('-', ' ', '\r')) igenom varje rad och
            // tar bort inledande eller avslutande streck, blanksteg och returtecken,
            // 3. och ToList() samlar allt till en List<string>
            Suggestions = result.Value.Content[0].Text
                .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim('-', ' ', '\r'))
                .ToList();
        }
    }
}
