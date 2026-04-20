using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;

namespace KpiSessionSimulator.Teachers
{
    public class MatanTeacher : BasicTeacher
    {
        public MatanTeacher() : base("Пан Легеза", new TransferToFiotPenalty("ФІОТ"), "Матан") { }

        public override void Interact(Player player)
        {
            Console.WriteLine($"{Name}: Шановний! Готуйтеся до мого потрійного інтегралу!");

            //Додати сюди завантаження файлику готових питань
        }
    }
}
