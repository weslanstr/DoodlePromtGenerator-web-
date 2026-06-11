using DoodlePromptGenerator;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoodlePromptGenerator.Pages
{
    public class IndexModel : PageModel
    {
        private const int MaxPromptHistory = 30;
        private const string PromptHistoryKey = "PromptHistory";
        private readonly PromptBuilder _promptBuilder = new();

        public string Output { get; set; } = "";
        public string PromptText { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public int PromptCount { get; set; }

        [BindProperty(SupportsGet = true)]
        public int AsciiWidth { get; set; } = 100;

        [BindProperty(SupportsGet = true)]
        public int AsciiHeight { get; set; } = 42;

        [BindProperty(SupportsGet = true)]
        public string? PromptId { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrWhiteSpace(PromptId))
            {
                Output = HttpContext.Session.GetString(GetPromptKey(PromptId)) ?? "";
                PromptText = HttpContext.Session.GetString(GetPromptTextKey(PromptId)) ?? "";
            }
        }

        public IActionResult OnPostGenerate()
        {
            PromptCount++;
            var prompt = PromptFormatter.BuildPromptWithAscii(_promptBuilder, AsciiWidth, AsciiHeight);
            Output = prompt.Display;
            PromptText = prompt.Text;

            PromptId = Guid.NewGuid().ToString("N");
            SavePrompt(PromptId, Output, PromptText);

            return RedirectToPage(new
            {
                PromptId,
                PromptCount,
                AsciiWidth,
                AsciiHeight
            });
        }

        public string GetButtonText()
        {
            if (PromptCount == 0)
                return "Gimmi a art idea!";

            return $"Gimmi the {(PromptCount + 1).ToOrdinalWords()} art idea!";
        }

        private void SavePrompt(string promptId, string output, string promptText)
        {
            HttpContext.Session.SetString(GetPromptKey(promptId), output);
            HttpContext.Session.SetString(GetPromptTextKey(promptId), promptText);

            var history = HttpContext.Session.GetString(PromptHistoryKey)?
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .ToList() ?? [];

            history.Add(promptId);

            while (history.Count > MaxPromptHistory)
            {
                HttpContext.Session.Remove(GetPromptKey(history[0]));
                HttpContext.Session.Remove(GetPromptTextKey(history[0]));
                history.RemoveAt(0);
            }

            HttpContext.Session.SetString(PromptHistoryKey, string.Join(',', history));
        }

        private static string GetPromptKey(string promptId) => $"Prompt:{promptId}";
        private static string GetPromptTextKey(string promptId) => $"PromptText:{promptId}";
    }
}
