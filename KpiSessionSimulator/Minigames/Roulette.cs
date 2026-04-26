using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Minigames
{
    public class Roulette : IMiniGame
    {
        public const int BulletSlots = 6;
        public const int FreeSlots = 2;
        public int CurrBullets { get; private set; }

        public Roulette()
        {
            CurrBullets = 1;
        }

        public bool Play(Player player, BasicTeacher teacher, int numberOfQuestion)
        {
            if(numberOfQuestion > 0 && numberOfQuestion % 3 == 0)
            {
                CurrBullets++;
            }

            if(player.WrongAnswersStreak > 0 && player.WrongAnswersStreak % 3 == 0)
            {
                CurrBullets++;
            }

            if(CurrBullets > BulletSlots - FreeSlots)
            {
                CurrBullets = BulletSlots - FreeSlots;
            }

            Console.WriteLine("Ви чуєте прокрут барабану револьвера...");
            Console.WriteLine($"\n{teacher.Name}: Якщо не вчили, може, хоч це допоможе...");
            Console.WriteLine($"Кількість патронів: {CurrBullets}/{BulletSlots}");
            Console.WriteLine("(натисність будь-яку клавішу для пострілу)");
            Console.ReadKey();

            Random rnd = new Random();

            if(rnd.Next(1, BulletSlots + 1) > CurrBullets)
            {
                Console.WriteLine("Чутно металевий 'клац'...");
                Console.WriteLine($"\n{teacher.Name}: Сьогодні ваш другий День народження!");

                return true;
            }

            Console.WriteLine("\nПостріл...");

            PlayerStats curStats = player.Stats;
            curStats.Deaths++;
            player.Stats = curStats;

            return false;
        }
    }
}
