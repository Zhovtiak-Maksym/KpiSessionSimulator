using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Interfaces
{
    public interface IMiniGame
    {
        bool Play(Player player, BasicTeacher teacher, int numberOfQuestion);
    }
}
