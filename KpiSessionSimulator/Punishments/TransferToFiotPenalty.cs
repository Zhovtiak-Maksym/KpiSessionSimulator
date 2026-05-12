using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using Spectre.Console;

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
            AnsiConsole.MarkupLine($"\n[bold darkred]You were kicked out of your 'home' to {_transferToFaculty}! Think about your future...[/]");
            player.Faculty = _transferToFaculty;
        }
    }
}
