using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;
using KpiSessionSimulator.Core;
using Spectre.Console;

namespace KpiSessionSimulator.Teachers
{
    public class MatanTeacher : BasicTeacher
    {
        public MatanTeacher() : base("Mr. Leheza", new TransferToFiotPenalty("FIOT"), "Calculus") { }

        public override void Interact(Player player, ExamState state)
        {
            if (player.Stats.IsExpelled)
            {
                AnsiConsole.MarkupLine($"\n[cyan]{Name}:[/] [grey]You have been expelled, the only road left is Donbas.[/]");

                return;
            }

            AnsiConsole.MarkupLine($"\n[grey]You came to the exam to[/] [cyan]{Name}[/][grey], I hope you drank holy water...[/]");
            Thread.Sleep(GameSettings.ShortPauseMs);

            if (player.Stats.Deaths > 0)
            {
                AnsiConsole.MarkupLine($"\n[cyan]{Name}:[/] [yellow]I heard the ambulance took you away at the last exam? I hope today you won't faint from my integrals![/]");
                Thread.Sleep(GameSettings.ShortPauseMs);
            }

            if (Random.Shared.Next(1, 101) <= 30)
            {
                AnsiConsole.MarkupLine($"\n[cyan]{Name}:[/] [darkorange]You reek of fear. Increasing the difficulty of the questions.[/]");

                if (state.CurrentDifficulty == Difficulty.Easy)
                {
                    state.CurrentDifficulty = Difficulty.Normal;
                }
                else if (state.CurrentDifficulty == Difficulty.Normal)
                {
                    state.CurrentDifficulty = Difficulty.Medium;
                }

                Thread.Sleep(GameSettings.ShortPauseMs);
            }

            AnsiConsole.MarkupLine($"\n[cyan]{Name}:[/] [white]Dear student! Prepare for my triple integral![/]");
        }
    }
}
