using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Punishments
{
    public class SecondaryPenalty : IPunishment
    {
        public void DoPunishment(Player player)
        {
            var currentStats = player.Stats;

            if (currentStats.IsONSecondary)
            {
                AnsiConsole.MarkupLine("\n[bold red]You failed the retake. You are expelled, you better enroll in management[/]");
                currentStats.IsExpelled = true;

                player.Stats = currentStats;
                return;
            }

            AnsiConsole.MarkupLine("\n[bold darkorange]You failed the exam, you are sent to retake. Prepare to meet God[/]");
            currentStats.IsONSecondary = true;

            player.Stats = currentStats;
        }
    }
}
