using KpiSessionSimulator.Attributes;
using KpiSessionSimulator.Core;
using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;
using System.Reflection;

namespace KpiSessionSimulator.Shop
{
    public class ShopManager
    {
        private List<IItemCommand> _perks;

        public ShopManager()
        {
            _perks = new List<IItemCommand>();

            var itemTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t =>
                    t.GetCustomAttribute<ShopPerksAttribute>() != null &&
                    typeof(IItemCommand).IsAssignableFrom(t) &&
                    !t.IsInterface &&
                    !t.IsAbstract);

            foreach (var type in itemTypes)
            {
                var instance = (IItemCommand)Activator.CreateInstance(type);

                if (instance != null)
                {
                    _perks.Add(instance);
                }
            }
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

                var displayOptions = new Dictionary<string, IItemCommand>();

                foreach (var item in _perks)
                {
                    string nameCol = item.Name.PadRight(15);
                    string priceCol = $"{item.Price} t.".PadRight(7);

                    string row = $"[green]{nameCol}[/] | [gold1]{priceCol}[/] | [grey]{item.Description}[/]";

                    displayOptions.Add(row, item);
                }

                string exitOption = $"[red]{"Exit Shop".PadRight(15)}[/] | {"".PadRight(7)} | [grey]Leave the black market[/]";

                var allChoices = displayOptions.Keys.ToList();
                allChoices.Add(exitOption);

                var prompt = new SelectionPrompt<string>()
                    .Title("[yellow]Use UP/DOWN arrows to select a perk and press ENTER:[/]")
                    .PageSize(10)
                    .AddChoices(allChoices);

                string choice = AnsiConsole.Prompt(prompt);

                if (choice == exitOption)
                {
                    AnsiConsole.MarkupLine("\n[grey]Leaving the Black Market[/]");
                    Thread.Sleep(GameSettings.ShortPauseMs);

                    break;
                }

                var selectedPerk = displayOptions[choice];
                selectedPerk.Execute(player);

                AnsiConsole.MarkupLine("\n[grey](Press any key to continue)[/]");
                Console.ReadKey(true);
            }
        }
    }
}
