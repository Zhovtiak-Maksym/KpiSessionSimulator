using KpiSessionSimulator.Models;
using System.Text.Json;
using Spectre.Console;

namespace KpiSessionSimulator.Services
{
    public class ProfileManager
    {
        public static List<Player> LoadProfiles()
        {
            if (!File.Exists(PathsMacker.Profiles))
            {
                return new List<Player>();
            }

            string jsonStr = File.ReadAllText(PathsMacker.Profiles);

            return JsonSerializer.Deserialize<List<Player>>(jsonStr) ?? new List<Player>();
        }

        public static void SaveProfiles(List<Player> profiles)
        {
            PathsMacker.EnsureDataFolderExists();

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonStr = JsonSerializer.Serialize(profiles, options);
            File.WriteAllText(PathsMacker.Profiles, jsonStr);
        }

        public static Player LoginOrRegister()
        {
            List<Player> players = LoadProfiles();

            AnsiConsole.Write(
                new FigletText("KPI SESSION")
                    .Centered()
                    .Color(Color.Green));

            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\n[yellow]CHOOSE YOUR ACTION:[/]")
                        .AddChoices(new[] { "Login", "Register", "Exit Game" }));

                if (choice == "Exit Game")
                {
                    AnsiConsole.MarkupLine("[bold red]Exiting the simulator...[/]");
                    Environment.Exit(0);
                }

                Player result = null;

                if (choice == "Login")
                {
                    result = ProcessLogin(players);
                }
                else if (choice == "Register")
                {
                    result = ProcessRegister(players);
                }

                if (result != null)
                {
                    return result;
                }
            }
        }

        private static Player ProcessLogin(List<Player> allPlayers)
        {
            string nick = AnsiConsole.Ask<string>("[green]Enter nickname (or '0' to go back):[/] ");

            if (nick == "0")
            {
                return null;
            }

            Player existingPlayer = allPlayers.FirstOrDefault(p => p.NickName == nick);

            if (existingPlayer != null)
            {
                return ProcessPassword(existingPlayer);
            }
            else
            {
                AnsiConsole.MarkupLine("\n[bold yellow]Player not found. Switching to registration...[/]");

                return ProcessRegister(allPlayers, nick);
            }
        }

        private static Player ProcessPassword(Player existingPlayer)
        {
            while (true)
            {
                string password = AnsiConsole.Prompt(
                    new TextPrompt<string>("[blue]Enter password (or '0' to go back):[/] ")
                        .Secret());

                if (password == "0")
                {
                    return null;
                }

                if (existingPlayer.Password == password)
                {
                    AnsiConsole.MarkupLine($"\n[bold green]Welcome back, {existingPlayer.NickName}![/]");

                    return existingPlayer;
                }

                AnsiConsole.MarkupLine("[bold red]Incorrect password![/]");
            }
        }

        private static Player ProcessRegister(List<Player> allPlayers, string defaultNick = null)
        {
            string nick = defaultNick;

            if (string.IsNullOrWhiteSpace(nick))
            {
                nick = AnsiConsole.Ask<string>("[green]Create a nickname (or '0' to go back):[/] ");
            }

            if (nick == "0")
            {
                return null;
            }

            while (allPlayers.Any(p => p.NickName == nick))
            {
                AnsiConsole.MarkupLine("\n[bold red]This nickname is already taken![/]");
                nick = AnsiConsole.Ask<string>("[green]Choose another nickname (or '0' to go back):[/] ");

                if (nick == "0")
                {
                    return null;
                }
            }

            string password = AnsiConsole.Prompt(
                new TextPrompt<string>("[blue]Create a password (or '0' to go back):[/] ")
                    .Secret());

            if (password == "0")
            {
                return null;
            }

            return CreateNewPlayer(allPlayers, nick, password);
        }

        private static Player CreateNewPlayer(List<Player> players, string nick, string pass)
        {
            string finalNick = nick;

            if (string.IsNullOrWhiteSpace(finalNick))
            {
                int counter = 1;
                finalNick = $"Student_{counter}";

                while (players.Any(p => p.NickName == finalNick))
                {
                    counter++;
                    finalNick = $"Student_{counter}";
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

            AnsiConsole.MarkupLine($"\n[bold green]Profile '{finalNick}' created![/]");

            return newPlayer;
        }
    }
}