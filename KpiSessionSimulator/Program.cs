using System.Text;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Core;
using KpiSessionSimulator.Factories;
using KpiSessionSimulator.Services;
using KpiSessionSimulator.Shop;
using Spectre.Console;

namespace KpiSessionSimulator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = Encoding.UTF8;

                Player currentPlayer = await ProfileManager.LoginOrRegisterAsync();

                if (currentPlayer == null)
                {
                    AnsiConsole.MarkupLine("[bold red]Authentication cancelled. Exiting game...[/]");

                    return;
                }

                await RunGameHubAsync(currentPlayer);
            }
            catch (Exception ex)
            {
                AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
                AnsiConsole.MarkupLine("\n[bold red]A critical error occurred. Program will be terminated[/]");
                Console.ReadKey(true);
            }
        }

        private static async Task RunGameHubAsync(Player player)
        {
            ShopManager shop = new ShopManager();

            while (true)
            {
                AnsiConsole.Clear();

                AnsiConsole.Write(
                    new FigletText("KPI HUB")
                        .Centered()
                        .Color(Color.Cyan1));

                AnsiConsole.MarkupLine($"\n[bold]Student:[/] [green]{player.NickName}[/] | [bold]Tokens:[/] [gold1]{player.Stats.Tokens}[/]\n");

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]Where do you want to go?[/]")
                        .AddChoices(new[]
                        {
                            "Go to Exam",
                            "Black Market",
                            "View My Stats",
                            "Save and Exit"
                        }));

                if (choice == "Go to Exam")
                {
                    await PlayExamAsync(player);
                }
                else if (choice == "Black Market")
                {
                    shop.ShopLife(player);
                    await SavePlayerProgressAsync(player);
                }
                else if (choice == "View My Stats")
                {
                    ShowPlayerStats(player);
                }
                else if (choice == "Save and Exit")
                {
                    await SavePlayerProgressAsync(player);
                    AnsiConsole.MarkupLine("\n[bold green]Progress saved[/]");

                    break;
                }
            }
        }

        private static async Task PlayExamAsync(Player player)
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[yellow]CHOOSE SUBJECT FOR THE EXAM:[/]")
                    .AddChoices(new[]
                    {
                        "OP (Skostariev)",
                        "ASD (Sulema)",
                        "Matan (Leheza)"
                    }));

            ExamData data = await ExamFactory.GetExamSetUpAsync(choice);

            ProgramProcess game = new ProgramProcess(player, data.Teacher, data.Questions);
            game.Exam();

            AnsiConsole.WriteLine();

            var table = new Table()
                .Border(TableBorder.Rounded)
                .Title("[yellow]EXAM RESULTS[/]")
                .AddColumn(new TableColumn("[grey]Metric[/]").Centered())
                .AddColumn(new TableColumn("[grey]Value[/]").Centered());

            table.AddRow("Player", $"[blue]{player.NickName}[/]");
            table.AddRow("Expelled", player.Stats.IsExpelled ? "[red]YES[/]" : "[green]NO[/]");
            table.AddRow("Minigame Deaths", $"[red]{player.Stats.Deaths}[/]");
            table.AddRow("Tokens Amount", $"[gold1]{player.Stats.Tokens}[/]");

            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("\n[grey](Press any key to return to HUB)[/]");
            Console.ReadKey(true);

            await SavePlayerProgressAsync(player);
        }

        private static async Task SavePlayerProgressAsync(Player player)
        {
            var allProfiles = await ProfileManager.LoadProfilesAsync();
            int index = allProfiles.FindIndex(p => p.NickName == player.NickName);

            if (index != -1)
            {
                allProfiles[index] = player;
            }
            else
            {
                allProfiles.Add(player);
            }

            await ProfileManager.SaveProfilesAsync(allProfiles);
        }

        private static void ShowPlayerStats(Player player)
        {
            AnsiConsole.Clear();

            var table = new Table()
                .Border(TableBorder.DoubleEdge)
                .Title($"[cyan]Statistics of {player.NickName}[/]");

            table.AddColumn("Metric");
            table.AddColumn("Value");

            table.AddRow("Faculty", $"[blue]{player.Faculty}[/]");
            table.AddRow("Passed Exams", $"[green]{player.Stats.PassedExams}[/]");
            table.AddRow("Deaths", $"[red]{player.Stats.Deaths}[/]");
            table.AddRow("Tokens", $"[gold1]{player.Stats.Tokens}[/]");
            table.AddRow("Expelled", player.Stats.IsExpelled ? "[red]YES[/]" : "[green]NO[/]");

            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("\n[grey](Press any key to return to menu)[/]");
            Console.ReadKey(true);
        }
    }
}
