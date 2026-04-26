using System.Text;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;
using KpiSessionSimulator.Core;

namespace KpiSessionSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            Console.WriteLine("---------------------------------------");
            Console.WriteLine("    Симулятор Сесії КПІ     ");
            Console.WriteLine("---------------------------------------");

            Console.Write("\nВведи своє ім'я: ");
            string playerName = Console.ReadLine();

            Player currentPlayer = new Player
            {
                NickName = string.IsNullOrWhiteSpace(playerName) ? "Студент" : playerName
            };

            BasicTeacher opTeacher = new OpTeacher();

            ProgramProcess game = new ProgramProcess(currentPlayer, opTeacher);

            game.Exam();

            Console.WriteLine("\n---------------------------------------");
            Console.WriteLine("           ФІНАЛЬНА СТАТИСТИКА         ");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Гравець: {currentPlayer.NickName}");
            Console.WriteLine($"Відрахований: {(currentPlayer.IsExpelled ? "ТАК" : "НІ")}");
            Console.WriteLine($"Кількість смертей у мінііграх: {currentPlayer.Stats.Deaths}");
            Console.WriteLine("-----------------------------------------");

            Console.WriteLine("\nНатисни будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
