using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;
using KpiSessionSimulator.Attributes;

namespace KpiSessionSimulator.Shop
{
    [ShopPerks]
    public class EagleEye : IItemCommand
    {
        public string Name => "Blue Eye";

        public int Price => 300;

        public string Description => "Improves your vision due to drinking alcohol on Polyana, now you can see not only squirrels but also the wrong answer";

        public void Execute(Player player)
        {
            if (player.Stats.Tokens < Price)
            {
                AnsiConsole.MarkupLine($"\n[bold red]Not enough tokens to buy {Name}[/]");
                return;
            }

            player.Stats.Tokens -= Price;
            player.Stats.EagleEyeCount++;

            AnsiConsole.MarkupLine($"\n[bold green]{Name} has been bought[/]");
        }
    }
}
