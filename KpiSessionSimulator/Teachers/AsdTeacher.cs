using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;
using KpiSessionSimulator.Core;

namespace KpiSessionSimulator.Teachers
{
    public class ASDTeacher : BasicTeacher
    {
        private const int PenaltyAmount = 300;
        private const int ShortPauseMs = 1500;

        public ASDTeacher() : base("Ольга Костянтинівна", new TokenPenalty(PenaltyAmount), "АСД") { }

        public override void Interact(Player player, ExamState state)
        {
            if (player.Stats.IsExpelled)
            {
                Console.WriteLine($"\n{Name}: Вас було відраховано! Краще почитайте мій новий роман...");
                return;
            }

            Console.WriteLine($"\nВи прийшли на екзамен до {Name} з предмету '{Subject}'");
            Thread.Sleep(ShortPauseMs);

            if (player.Stats.Deaths >= 1)
            {
                Console.WriteLine($"\n{Name}: Я відчуваю, що ви спершу пишете код, а потім будуєте блок-схему. Додаю ще одну кулю у револьвер!");
                state.CurrentDifficulty = Difficulty.Difficult;
                Thread.Sleep(ShortPauseMs);
            }

            Console.WriteLine($"\n{Name}: Панове, екзамен нас розсудить...");
        }
    }
}
