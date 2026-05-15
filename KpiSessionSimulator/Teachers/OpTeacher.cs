using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;
using KpiSessionSimulator.Core;
using Spectre.Console;

namespace KpiSessionSimulator.Teachers
{
    public class OpTeacher : BasicTeacher
    {
        public OpTeacher() : base("Ihor Vitaliyovych", new SecondaryPenalty(), "OP") { }

        public override void Interact(Player player, ExamState state)
        {
            if (player.Stats.IsExpelled)
            {
                AnsiConsole.MarkupLine($"\n[cyan]{Name}:[/] [grey]Go with God. You have already been expelled[/]");

                return;
            }

            AnsiConsole.MarkupLine($"\n[grey]You came to the exam to[/] [cyan]{Name}[/] [grey]for the subject '{Subject}'[/]");
            Thread.Sleep(GameSettings.ShortPauseMs);

            if (player.Stats.Deaths > 0)
            {
                AnsiConsole.MarkupLine($"\n[cyan]{Name}:[/] [yellow]I see the session is hard for you... Let me just give you one plus in advance, but for this you will solve 20 LeetCode problems[/]");
                state.CorrectAnswers = 1;
                Thread.Sleep(GameSettings.ShortPauseMs);
            }
            else
            {
                AnsiConsole.MarkupLine($"\n[cyan]{Name}:[/] [white]Come on, finish me already...[/]");
            }
        }
    }
}
