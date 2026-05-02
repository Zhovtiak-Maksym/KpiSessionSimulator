using KpiSessionSimulator.Models;
using KpiSessionSimulator.Punishments;
using KpiSessionSimulator.Core;

namespace KpiSessionSimulator.Teachers
{
    public class MatanTeacher : BasicTeacher
    {
        private const int ShortPauseMs = 1500;
        private Random _rnd = new Random();

        public MatanTeacher() : base("Пан Легеза", new TransferToFiotPenalty("ФІОТ"), "Матан") { }

        public override void Interact(Player player, ExamState state)
        {
            if (player.IsExpelled)
            {
                Console.WriteLine($"\n{Name}: Вас було відраховано, єдина дорога - Донбас");
                return;
            }

            Console.WriteLine($"\nВи прийшли на екзамен до {Name}, сподіваюся ви випили святої води...");
            Thread.Sleep(ShortPauseMs);

            if (player.Stats.Deaths > 0)
            {
                Console.WriteLine($"\n{Name}: Я чув, що вас забрала швидка на минулому іспиті? Сподіваюсь, сьогодні ви не знепритомнієте від моїх інтегралів!");
                Thread.Sleep(ShortPauseMs);
            }

            if (_rnd.Next(1, 101) <= 30)
            {
                Console.WriteLine($"\n{Name}: Від вас віє страхом. Підвищую складність питань");

                if (state.CurrentDifficulty == Difficulty.Easy)
                    state.CurrentDifficulty = Difficulty.Normal;
                else if (state.CurrentDifficulty == Difficulty.Normal)
                    state.CurrentDifficulty = Difficulty.Medium;

                Thread.Sleep(ShortPauseMs);
            }

            Console.WriteLine($"\n{Name}: Шановний! Готуйтеся до мого потрійного інтегралу!");
        }
    }
}
