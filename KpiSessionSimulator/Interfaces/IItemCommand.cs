using KpiSessionSimulator.Models;

namespace KpiSessionSimulator.Interfaces
{
    public interface IItemCommand
    {
        string Name { get; }
        int Price { get; }
        string Description { get; }
        void Execute(Player player);
    }
}
