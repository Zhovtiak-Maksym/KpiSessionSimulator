using System.Text;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Core;
using KpiSessionSimulator.Factories;

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

            Console.WriteLine("\nОберіть предмет для складання екзамену:");
            Console.WriteLine("1. ОП (Скостарєв Ігор Віталійович)");
            Console.WriteLine("2. АСД (Сулема Ольга Костянтинівна)");
            Console.WriteLine("3. Матан (Пан Легеза)");

            string choice = Console.ReadLine();

            ExamData data = ExamFactory.GetExamSetUp(choice);

            ProgramProcess game = new ProgramProcess(currentPlayer, data.Teacher, data.Questions);

            game.Exam();

            Console.WriteLine("\n---------------------------------------");
            Console.WriteLine("           ФІНАЛЬНА СТАТИСТИКА         ");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"Гравець: {currentPlayer.NickName}");
            Console.WriteLine($"Відрахований: {(currentPlayer.Stats.IsExpelled ? "ТАК" : "НІ")}");
            Console.WriteLine($"Кількість смертей у мінііграх: {currentPlayer.Stats.Deaths}");
            Console.WriteLine("-----------------------------------------");

            Console.WriteLine("\nНатисни будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
