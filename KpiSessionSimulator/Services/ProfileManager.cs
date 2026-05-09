using KpiSessionSimulator.Models;
using System.Text.Json;

namespace KpiSessionSimulator.Services
{
    public class ProfileManager
    {
        private const string ProfilesPath = "Data/profiles.json";

        public static List<Player> LoadProfiles()
        {
            if (!File.Exists(ProfilesPath))
            {
                return new List<Player>();
            }

            string jsonStr = File.ReadAllText(ProfilesPath);

            return JsonSerializer.Deserialize<List<Player>>(jsonStr) ?? new List<Player>();
        }

        public static void SaveProfiles(List<Player> profiles)
        {
            Directory.CreateDirectory("Data");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string jsonStr = JsonSerializer.Serialize(profiles, options);
            File.WriteAllText(ProfilesPath, jsonStr);
        }

        public static Player LoginOrRegister()
        {
            List<Player> allPlayers = LoadProfiles();

            Console.WriteLine("\nСимулятор Сесії КПІ");
            Console.WriteLine("1. Увійти");
            Console.WriteLine("2. Зареєструватись");
            Console.Write("Ваш вибір: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Нікнейм: ");
                string nick = Console.ReadLine();

                Player existingPlayer = allPlayers.FirstOrDefault(p => p.NickName == nick);

                if (existingPlayer != null)
                {
                    while (true)
                    {
                        Console.Write("Пароль: ");
                        string pass = Console.ReadLine();

                        if (existingPlayer.Password == pass)
                        {
                            Console.WriteLine($"\nЗ поверненням, {nick}!");

                            return existingPlayer;
                        }
                        else
                        {
                            Console.WriteLine("Невірний пароль! Спробуйте ще раз");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nГравця не знайдено! Переходимо до реєстрації...");

                    return RegisterNewPlayer(allPlayers, nick);
                }
            }
            else
            {
                Console.Write("Придумайте нікнейм: ");
                string nick = Console.ReadLine();

                return RegisterNewPlayer(allPlayers, nick);
            }
        }

        private static Player RegisterNewPlayer(List<Player> players, string nick)
        {
            while (players.Any(p => p.NickName == nick))
            {
                Console.WriteLine("\nЦей нікнейм вже зайнятий іншим студентом!");
                Console.Write("Придумайте інший нікнейм: ");
                nick = Console.ReadLine();
            }

            Console.Write("Придумайте пароль: ");
            string password = Console.ReadLine();

            return CreateNewPlayer(players, nick, password);
        }

        private static Player CreateNewPlayer(List<Player> players, string nick, string pass)
        {
            string finalNick = nick;

            if (string.IsNullOrWhiteSpace(finalNick))
            {
                int counter = 1;
                finalNick = $"Студент({counter})";

                while (players.Any(p => p.NickName == finalNick))
                {
                    counter++;
                    finalNick = $"Студент({counter})";
                }
            }

            Player newPlayer = new Player
            {
                NickName = finalNick,
                Password = pass,
                Stats = new PlayerStats()
            };

            players.Add(newPlayer);
            SaveProfiles(players);

            Console.WriteLine($"\nПрофіль для '{finalNick}' створено");

            return newPlayer;
        }
    }
}
