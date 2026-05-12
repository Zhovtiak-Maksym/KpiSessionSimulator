using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Punishments
{
    public class SecondaryPenalty : IPunishment
    {
        public void DoPunishment(Player player)
        {
            if (player.Stats.IsONSecondary)
            {
                AnsiConsole.MarkupLine("\n[bold red]You failed the retake. You are expelled, you better enroll in management...[/]");
                player.Stats.IsExpelled = true;

                return;
            }

            AnsiConsole.MarkupLine("\n[bold darkorange]You failed the exam, you are sent to retake. Prepare to meet God...[/]");
            player.Stats.IsONSecondary = true;
        }
    }
}
