using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Minigames
{
    public class Roulette : IMiniGame
    {
        public const int BulletSlots = 6;
        public const int MaxBullets = 4;
        public const int ShortPause = 1500;
        public int CurrBullets { get; private set; }

        public Roulette()
        {
            CurrBullets = 2;
        }

        public bool Play(Player player, BasicTeacher teacher, int numberOfQuestion)
        {
            Console.WriteLine("\nВи чуєте прокрут барабану револьвера...");
            Console.WriteLine($"\n{teacher.Name}: Якщо не вчили, може хоч це допоможе...");
            Console.WriteLine($"Кількість патронів: {CurrBullets}/{BulletSlots}");

            bool hasImmunity = UseImmunity(player);

            Console.WriteLine("\n(натисніть будь-яку клавішу для пострілу)");
            Console.ReadKey(true); 

            bool isShot = CheckShot();

            if (!isShot)
            {
                Console.WriteLine("\nЧутно металевий 'клац'...");
                Console.WriteLine($"\n{teacher.Name}: Сьогодні ваш другий День народження!");

                if (CurrBullets < MaxBullets)
                {
                    CurrBullets++;
                    Console.WriteLine($"Кількість патронів у рулетці збільшено до {CurrBullets}");
                }

                return true; 
            }
            else
            {
                Console.WriteLine("\nПостріл...");
                Thread.Sleep(ShortPause);

                if (hasImmunity)
                {
                    Console.WriteLine("Куля у вашій голові починає розчинятися, і рана загоюється...");

                    return true; 
                }
                else
                {
                    Console.WriteLine($"Вас забирає швидка. {teacher.Name} тихо ховає револьвер у стіл.");

                    PlayerStats curStats = player.Stats;
                    curStats.Deaths++;
                    player.Stats = curStats;

                    return false; 
                }
            }
        }

        private bool UseImmunity(Player player)
        {
            if (player.Stats.ImmunityCount > 0)
            {
                Console.Write($"\nСкористатися 'Імунітетом' (так/ні): ");
                string choice = Console.ReadLine()?.Trim().ToLower();

                if (choice == "так")
                {
                    player.Stats.ImmunityCount--;

                    return true;
                }
            }

            return false;
        }

        private bool CheckShot()
        {
            Random rnd = new Random();

            if (rnd.Next(1, BulletSlots + 1) <= CurrBullets)
            {
                return true; 
            }

            return false;
        }
    }
}
