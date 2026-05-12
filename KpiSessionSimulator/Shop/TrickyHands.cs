using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Shop
{
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

            player.Stats.Tokens -= Price;
            player.Stats.TrickyHandsCount++;

            AnsiConsole.MarkupLine($"\n[bold green]{Name} has been bought[/]"); ;
        }
    }
}
