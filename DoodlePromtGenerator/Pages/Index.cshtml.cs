using DoodlePromptGenerator;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoodlePromptGenerator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PromptBuilder _promptBuilder = new();

        [BindProperty]
        public string Output { get; set; } = "";

        [BindProperty]
        public int PromptCount { get; set; }

        public void OnGet()
        {
            PromptCount = 0;
        }

        public void OnPostGenerate()
        {
            PromptCount++;
            Output = PromptFormatter.BuildPromptWithAscii(_promptBuilder);
        }

        public string GetButtonText()
        {
            if (PromptCount == 0)
                return "Gimmi a art prompt!";

            return $"Gimmi a {(PromptCount + 1).ToOrdinalWords()} art prompt!";
        }

        private string ToOrdinalWords(int number)
        {
            return number switch
            {
                1 => "First",
                2 => "Second",
                3 => "Third",
                4 => "Fourth",
                5 => "Fifth",
                6 => "Sixth",
                7 => "Seventh",
                8 => "Eighth",
                9 => "Ninth",
                10 => "Tenth",
                _ => $"{number}th"
            };
        }
    }
}