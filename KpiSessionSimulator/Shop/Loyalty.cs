using KpiSessionSimulator.Attributes;
using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Shop
{
    [ShopPerks]
    public class Loyalty : IItemCommand
    {
        private const string PerkName = "Loyalty";
        private const int PerkPrice = 200;
        private const string PerkDescription = "You finally took a shower, your charisma and appearance have significantly improved, the teacher will definitely give you the right to change the ticket";

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
            stats.LoyaltyCount++;
            player.Stats = stats;

            AnsiConsole.MarkupLine($"\n[bold green]{Name} has been bought[/]"); ;
        }
    }
}
