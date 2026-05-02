using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;
using KpiSessionSimulator.Core;

namespace KpiSessionSimulator.Teachers
{
    public class OpTeacher : BasicTeacher
    {
        private const int ShortPauseMs = 1500;

        public OpTeacher() : base("Ігор Віталійович", new SecondaryPenalty(), "ОП") { }

        public override void Interact(Player player, ExamState state)
        {
            if (player.IsExpelled)
            {
                Console.WriteLine($"\n{Name}: Йдіть з Богом. Вас вже відрахували");
                return;
            }

            Console.WriteLine($"\nВи прийшли на екзамен до {Name} з предмету '{Subject}'");
            Thread.Sleep(ShortPauseMs);

            if (player.Stats.Deaths > 0)
            {
                Console.WriteLine($"\n{Name}: Бачу, сесія важко вам дається... Давайте я просто поставлю вам один плюс авансом, але за це ви розв'яжете 20 задачок на літ код");
                state.CorrectAnswers = 1; 
                Thread.Sleep(ShortPauseMs);
            }
            else
            {
                Console.WriteLine($"\n{Name}: Давайте добийте мене вже...");
            }
        }
    }
}
