using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;
using KpiSessionSimulator.Core;
using Spectre.Console;

namespace KpiSessionSimulator.Teachers
{
    public class ASDTeacher : BasicTeacher
    {
        private const int PenaltyAmount = 300;
        private const int ShortPauseMs = 1500;

        public ASDTeacher() : base("Olha Kostiantynivna", new TokenPenalty(PenaltyAmount), "ASD") { }

        public override void Interact(Player player, ExamState state)
        {
            if (player.Stats.IsExpelled)
            {
                AnsiConsole.MarkupLine($"\n[cyan]{Name}:[/] [grey]You have been expelled! Better read my new novel...[/]");

                return;
            }

            AnsiConsole.MarkupLine($"\n[grey]You came to the exam to[/] [cyan]{Name}[/] [grey]for the subject '{Subject}'[/]");
            Thread.Sleep(ShortPauseMs);

            if (player.Stats.Deaths >= 1)
            {
                AnsiConsole.MarkupLine($"\n[cyan]{Name}:[/] [red]I feel that you write code first and then build the flowchart. Difficulty has been increased![/]");
                state.CurrentDifficulty = Difficulty.Difficult;
                Thread.Sleep(ShortPauseMs);
            }

            AnsiConsole.MarkupLine($"\n[cyan]{Name}:[/] [white]Gentlemen, the exam will judge us...[/]");
        }
    }
}
