using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;
using KpiSessionSimulator.Attributes;

namespace KpiSessionSimulator.Shop
{
    [ShopPerks]
    public class EagleEye : IItemCommand
    {
        private const string PerkName = "Blue Eye";
        private const int PerkPrice = 300;
        private const string PerkDescription = "Improves your vision due to drinking alcohol on Polyana, now you can see not only squirrels but also the wrong answer";

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
            stats.EagleEyeCount++;
            player.Stats = stats;

            AnsiConsole.MarkupLine($"\n[bold green]{Name} has been bought[/]");
        }
    }
}
