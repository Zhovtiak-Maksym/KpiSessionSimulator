using System.Text;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Core;
using KpiSessionSimulator.Factories;
using KpiSessionSimulator.Services;

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

            Player currentPlayer = ProfileManager.LoginOrRegister();

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
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Гравець: {currentPlayer.NickName}");
            Console.WriteLine($"Відрахований: {(currentPlayer.Stats.IsExpelled ? "ТАК" : "НІ")}");
            Console.WriteLine($"Кількість смертей у мінііграх: {currentPlayer.Stats.Deaths}");
            Console.WriteLine($"Залишок токенів: {currentPlayer.Stats.Tokens}");
            Console.WriteLine("---------------------------------------");

            var allProfiles = ProfileManager.LoadProfiles();

            int index = allProfiles.FindIndex(p => p.NickName == currentPlayer.NickName);

            if (index != -1)
            {
                allProfiles[index] = currentPlayer;
            }
            else
            {
                allProfiles.Add(currentPlayer);
            }

            ProfileManager.SaveProfiles(allProfiles);
            Console.WriteLine("\nПрогрес збережено");

            Console.WriteLine("\nНатисни будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}
