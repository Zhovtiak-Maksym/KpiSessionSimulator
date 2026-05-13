using KpiSessionSimulator.Attributes;
using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Shop
{
    [ShopPerks]
    public class Immunity : IItemCommand
    {
        public string Name => "Immunity";

        public int Price => 500;

        public string Description => "Due to a small scholarship, your digestive system has evolved, now your body can break down a bullet in the head into nutrients";

        public void Execute(Player player)
        {
            if (player.Stats.Tokens < Price)
            {
                AnsiConsole.MarkupLine($"\n[bold red]Not enough tokens to buy {Name}[/]");
                return;
            }

            var stats = player.Stats;
            stats.Tokens -= Price;
            stats.ImmunityCount++;
            player.Stats = stats;

            AnsiConsole.MarkupLine($"\n[bold green]{Name} has been bought[/]");
        }
    }
}
