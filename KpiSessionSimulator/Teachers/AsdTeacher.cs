using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;

namespace KpiSessionSimulator.Teachers
{
    public class ASDTeacher : BasicTeacher
    {
        private const int PenaltyAmount = 300; //Можна створити файлик з параметрами викладачів
        public ASDTeacher() : base("Ольга Костянтинівна", new TokenPenalty(PenaltyAmount), "АСД") { }
        public override void Interact(Player player)
        {
            Console.WriteLine($"{Name}: Панове, екзамен нас розсудить...");
        }
    }
}
