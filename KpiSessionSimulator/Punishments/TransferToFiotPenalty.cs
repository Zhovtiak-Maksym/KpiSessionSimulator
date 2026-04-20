using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Punishments
{
    internal class TransferToFiotPenalty : IPunishment
    {
        private readonly string _transferToFaculty;

        public TransferToFiotPenalty(string transferToFaculty)
        {
            _transferToFaculty = transferToFaculty;
        }

        public void DoPunishment(Player player)
        {
            Console.WriteLine($"Тебе вигнали з 'рідного' дому на {_transferToFaculty}! Подумай про своє майбутнє...");
            player.Faculty = _transferToFaculty;
        }
    }
}
