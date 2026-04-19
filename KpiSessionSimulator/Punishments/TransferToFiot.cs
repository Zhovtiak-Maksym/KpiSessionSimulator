using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Punishments
{
    internal class TransferToFiot : IPunishment
    {
        private readonly string _transferToFaculty;

        public TransferToFiot(string transferToFaculty)
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
