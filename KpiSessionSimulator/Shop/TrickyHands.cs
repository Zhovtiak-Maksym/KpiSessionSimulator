using KpiSessionSimulator.Attributes;
using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Shop
{
    [ShopPerks]
    public class TrickyHands : IItemCommand
    {
        public string Name => "Tricky Hands";

        public int Price => 150;

        public string Description => "Submitting tasks 5 minutes before the deadline taught your hands unprecedented agility to sneakily drop a card off the table";

        public void Execute(Player player)
        {
            if (player.Stats.Tokens < Price)
            {
                AnsiConsole.MarkupLine($"\n[bold red]Not enough tokens to buy {Name}[/]");
                return;
            }

            var stats = player.Stats;
            stats.Tokens -= Price;
            stats.TrickyHandsCount++;
            player.Stats = stats;

            AnsiConsole.MarkupLine($"\n[bold green]{Name} has been bought[/]"); ;
        }
    }
}
