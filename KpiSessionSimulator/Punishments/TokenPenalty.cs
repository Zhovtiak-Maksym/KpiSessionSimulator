using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;

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

            Console.WriteLine($"Ви втратили {_penaltyAmount} токенів!");
            player.Stats = curStats;
        }
    }
}
