using DoodlePromptGenerator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DoodlePromptGenerator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PromptBuilder _promptBuilder;

        public IndexModel()
        {
            _promptBuilder = new PromptBuilder();
        }

        [BindProperty]
        public string Output { get; set; } = "";

        public void OnGet()
        {
        }

        public void OnPostGenerate()
        {
            Output = PromptFormatter.BuildPromptWithAscii(_promptBuilder);
        }
    }
}