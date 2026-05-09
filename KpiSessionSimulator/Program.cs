using System.Text;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Core;
using KpiSessionSimulator.Factories;
using KpiSessionSimulator.Services;
using Spectre.Console;

namespace KpiSessionSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Player currentPlayer = ProfileManager.LoginOrRegister();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[yellow]CHOOSE SUBJECT FOR THE EXAM:[/]")
                    .AddChoices(new[]
                    {
                        "OP (Skostariev)",
                        "ASD (Sulema)",
                        "Calculus (Leheza)"
                    }));

            ExamData data = ExamFactory.GetExamSetUp(choice);

            ProgramProcess game = new ProgramProcess(currentPlayer, data.Teacher, data.Questions);
            game.Exam();

            AnsiConsole.WriteLine();
            var table = new Table()
                .Border(TableBorder.Rounded)
                .Title("[yellow]FINAL STATISTICS[/]")
                .AddColumn(new TableColumn("[grey]Metric[/]").Centered())
                .AddColumn(new TableColumn("[grey]Value[/]").Centered());

            table.AddRow("Player", $"[blue]{currentPlayer.NickName}[/]");
            table.AddRow("Expelled", currentPlayer.Stats.IsExpelled ? "[red]YES[/]" : "[green]NO[/]");
            table.AddRow("Minigame Deaths", $"[red]{currentPlayer.Stats.Deaths}[/]");
            table.AddRow("Tokens Remaining", $"[gold1]{currentPlayer.Stats.Tokens}[/]");

            AnsiConsole.Write(table);

            var allProfiles = ProfileManager.LoadProfiles();
            int index = allProfiles.FindIndex(p => p.NickName == currentPlayer.NickName);

            if (index != -1)
            {
                allProfiles[index] = currentPlayer;
            }
            else
            {
                allProfiles.Add(currentPlayer);
            }

            ProfileManager.SaveProfiles(allProfiles);
            AnsiConsole.MarkupLine("\n[bold green]Progress successfully saved![/]");

            AnsiConsole.MarkupLine("\n[grey]Press any key to exit...[/]");
            Console.ReadKey();
        }
    }
}
