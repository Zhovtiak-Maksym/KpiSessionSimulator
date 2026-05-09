using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Minigames
{
    public class Blackjack : IMiniGame
    {
        private Random Rnd = new Random();

        public bool Play(Player player, BasicTeacher teacher, int numberOfQuestion)
        {
            int scorePlayer = Rnd.Next(1, 11);
            int scoreTeacher = Rnd.Next(1, 11);

            Console.WriteLine($"\nРахунок {player.NickName}: {scorePlayer}");
            Console.WriteLine($"Рахунок {teacher.Name}: {scoreTeacher}");

            scorePlayer = PlayerTurn(player, scorePlayer);

            if (scorePlayer > 21)
            {
                return false;
            }

            Console.WriteLine($"\n{teacher.Name} набирає");

            scoreTeacher = TeacherTurn(teacher, scoreTeacher);

            if (scoreTeacher > 21)
            {
                return true;
            }

            return SelectWinner(player, teacher, scorePlayer, scoreTeacher);
        }


        private int PlayerTurn(Player player, int curScore)
        {
            while (true)
            {
                Console.WriteLine("\nБільше, чи 'Чек'?(1/2)");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    int newCard = Rnd.Next(1, 11);
                    curScore += newCard;
                    Console.WriteLine($"\nРахунок {player.NickName}: {curScore}");

                    if (curScore > 21)
                    {
                        if (player.Stats.TrickyHandsCount > 0 && UseTrickyHands(player, ref curScore, newCard))
                        {
                            continue; 
                        }

                        Console.WriteLine("Перебір!");
                        return curScore;
                    }
                }
                else if (input == "2")
                {
                    return curScore;
                }
                else
                {
                    Console.WriteLine("Такої опції не існує!");
                }
            }
        }

        private int TeacherTurn(BasicTeacher teacher, int curScore)
        {
            while (curScore < 17)
            {
                curScore += Rnd.Next(1, 11);
                Console.WriteLine($"\nРахунок {teacher.Name}: {curScore}");

                if (curScore > 21)
                {
                    Console.WriteLine($"У {teacher.Name} перебір. Перемога!");

                    return curScore;
                }
            }

            return curScore;
        }

        private bool SelectWinner(Player player, BasicTeacher teacher, int scorePlayer, int scoreTeacher)
        {
            Console.WriteLine("\nРезультати:");
            Console.WriteLine($"{player.NickName}: {scorePlayer}");
            Console.WriteLine($"{teacher.Name}: {scoreTeacher}");

            if (scorePlayer > scoreTeacher)
            {
                Console.WriteLine("Перемога!");

                return true;
            }
            else
            {
                Console.WriteLine("Викладач переміг. Оцінка знижена");

                return false;
            }
        }

        private bool UseTrickyHands(Player player, ref int score, int lastCard)
        {
            Console.Write($"\nСкористатися 'Спритними руками' (так/ні): ");
            string choice = Console.ReadLine()?.Trim().ToLower();

            if (choice == "так")
            {
                player.Stats.TrickyHandsCount--;
                score -= lastCard;

                Console.WriteLine($"\nВи непомітно скидаєте карту зі столу...");
                Console.WriteLine($"Рахунок {player.NickName}: {score}");

                return true; 
            }

            return false; 
        }
    }
}