using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Punishments
{
    public class TokenPenalty : IPunishment
    {
        private readonly int _penaltyAmount;

        public TokenPenalty(int penaltyAmount)
        {
            _penaltyAmount = penaltyAmount;
        }

        public void DoPunishment(Player player)
        {
            PlayerStats curStats = player.Stats;
            curStats.Tokens -= _penaltyAmount;

            if (curStats.Tokens < 0)
            {
                curStats.Tokens = 0;
            }

            AnsiConsole.MarkupLine($"\n[bold red]You lost {_penaltyAmount} tokens![/]");
            player.Stats = curStats;
        }
    }
}
