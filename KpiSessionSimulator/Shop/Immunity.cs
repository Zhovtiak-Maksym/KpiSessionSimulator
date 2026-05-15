using KpiSessionSimulator.Attributes;
using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Shop
{
    [ShopPerks]
    public class Immunity : IItemCommand
    {
        private const string PerkName = "Immunity";
        private const int PerkPrice = 500;
        private const string PerkDescription = "Due to a small scholarship, your digestive system has evolved, now your body can break down a bullet in the head into nutrients";

        public string Name => PerkName;
        public int Price => PerkPrice;
        public string Description => PerkDescription;

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
