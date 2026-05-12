using Spectre.Console;

namespace KpiSessionSimulator.Services
{
    public static class UIHelper
    {
        public static bool AskYesNo(string title)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(title)
                    .AddChoices(new[] { "Yes", "No" }));

            return choice == "Yes";
        }
    }
}
