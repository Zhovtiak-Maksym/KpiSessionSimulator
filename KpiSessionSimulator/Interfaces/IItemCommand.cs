using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Interfaces
{
    public interface IItemCommand
    {
        void Execute(Player player);
    }
}
