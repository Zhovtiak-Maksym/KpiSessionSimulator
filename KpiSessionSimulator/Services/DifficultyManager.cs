using KpiSessionSimulator.Core;
using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Services
{
    public class DifficultyManager
    {
        public void IncreaseDifficulty(ExamState state)
        {
            switch (state.CurrentDifficulty)
            {
                case Difficulty.Easy:
                    state.CurrentDifficulty = Difficulty.Normal;
                    AnsiConsole.MarkupLine("\n[yellow]Difficulty increased to 'Normal'[/]");
                    break;
                case Difficulty.Normal:
                    state.CurrentDifficulty = Difficulty.Medium;
                    AnsiConsole.MarkupLine("\n[darkorange]The teacher asks additional questions. Difficulty is 'Medium'[/]");
                    break;
                case Difficulty.Medium:
                    state.CurrentDifficulty = Difficulty.Difficult;
                    AnsiConsole.MarkupLine("\n[red]The teacher frowns. The next questions will be very difficult![/]");
                    break;
                case Difficulty.Difficult:
                    state.CurrentDifficulty = Difficulty.DeathMode;
                    AnsiConsole.MarkupLine("\n[bold red]DEATH MODE! Pray to God[/]");
                    break;
                case Difficulty.DeathMode:
                    AnsiConsole.MarkupLine("\n[bold darkred]Difficulty is maxed out. Dopka is imminent...[/]");
                    break;
            }
        }
    }
}