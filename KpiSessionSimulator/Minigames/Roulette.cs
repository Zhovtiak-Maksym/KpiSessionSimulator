using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Minigames
{
    public class Roulette : IMiniGame
    {
        public const int BulletSlots = 6;
        public const int MaxBullets = 4;
        public int CurrBullets { get; private set; }

        public Roulette()
        {
            CurrBullets = 2;
        }

        public bool Play(Player player, BasicTeacher teacher, int numberOfQuestion)
        {
            Console.WriteLine("\nВи чуєте прокрут барабану револьвера...");
            Console.WriteLine($"\n{teacher.Name}: Якщо не вчили, може хоч це допоможе...");
            Console.WriteLine("(натисніть будь-яку клавішу для пострілу)");
            Console.WriteLine($"Кількість патронів: {CurrBullets}/{BulletSlots}");
            Console.ReadKey();

            Random rnd = new Random();

            if (rnd.Next(1, BulletSlots + 1) > CurrBullets)
            {
                Console.WriteLine("\nЧутно металевий 'клац'...");
                Console.WriteLine($"\n{teacher.Name}: Сьогодні ваш другий День народження!");

                if (CurrBullets < MaxBullets)
                {
                    CurrBullets++;
                    Console.WriteLine($"Ви вижили! Кількість патронів у рулетці збільшено до {CurrBullets}");
                }

                return true;
            }

            Console.WriteLine("\nПостріл...");
            Console.WriteLine($"Вас забирає швидка. {teacher.Name} тихо ховає револьвер у стіл.");

            PlayerStats curStats = player.Stats;
            curStats.Deaths++;
            player.Stats = curStats;

            return false;
        }
    }
}
