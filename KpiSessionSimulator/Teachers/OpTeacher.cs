using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;

namespace KpiSessionSimulator.Teachers
{
    public class OpTeacher : BasicTeacher
    {
        public OpTeacher() : base("Ігор Віталійович", new SecondaryPenalty(), "ОП") { }
        public override void Interact(Player player)
        {
            Console.WriteLine($"{Name}: Давайте добийте мене вже...");
        }
    }
}
