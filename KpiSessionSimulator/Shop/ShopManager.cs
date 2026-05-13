using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Shop
{
    public class ShopManager
    {
        private List<IItemCommand> _perks;
        public const int ShortPause = 1500;

        public ShopManager()
        {
            _perks = new List<IItemCommand>
            {
                new EagleEye(),
                new Immunity(),
                new Loyalty(),
                new TrickyHands()
            };
        }

        public void ShopLife(Player player)
        {
            while (true)
            {
                AnsiConsole.Clear();

                AnsiConsole.Write(
                    new FigletText("BLACK MARKET")
                        .Centered()
                        .Color(Color.Purple));

                AnsiConsole.MarkupLine($"\n[bold]Current Balance:[/] [gold1]{player.Stats.Tokens} tokens[/]\n");

                var table = new Table()
                    .Border(TableBorder.Rounded)
                    .BorderColor(Color.Purple)
                    .Title("[yellow]Available Perks[/]");

                table.AddColumn(new TableColumn("[green]Perk[/]").Centered());
                table.AddColumn(new TableColumn("[gold1]Price[/]").Centered());
                table.AddColumn("[grey]Description[/]");

                var choices = new List<string>();

                foreach (var item in _perks)
                {
                    table.AddRow($"[bold]{item.Name}[/]", $"[gold1]{item.Price}[/]", item.Description);
                    choices.Add(item.Name);
                }

                choices.Add("Exit Shop");

                AnsiConsole.Write(table);

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\n[yellow]Select a perk to buy:[/]")
                        .AddChoices(choices));

                if (choice == "Exit Shop")
                {
                    AnsiConsole.MarkupLine("\n[grey]Leaving the Black Market...[/]");
                    Thread.Sleep(ShortPause);

                    break;
                }

                var selectedPerk = _perks.First(p => p.Name == choice);
                selectedPerk.Execute(player);

                AnsiConsole.MarkupLine("\n[grey](Press any key to continue)[/]");
                Console.ReadKey(true);
            }
        }
    }
}
