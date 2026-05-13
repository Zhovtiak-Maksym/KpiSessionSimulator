using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Services
{
    public class ProfileManager
    {
        public static async Task<List<Player>> LoadProfilesAsync()
        {
            return await JsonRepository<List<Player>>.LoadAsync(PathsMacker.Profiles);
        }

        public static async Task SaveProfilesAsync(List<Player> profiles)
        {
            PathsMacker.EnsureDataFolderExists();

            await JsonRepository<List<Player>>.SaveAsync(PathsMacker.Profiles, profiles);
        }

        public static async Task<Player> LoginOrRegisterAsync()
        {
            List<Player> players = await LoadProfilesAsync();

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
                    result = await ProcessLoginAsync(players);
                }
                else if (choice == "Register")
                {
                    result = await ProcessRegisterAsync(players);
                }

                if (result != null)
                {
                    return result;
                }
            }
        }

        private static async Task<Player> ProcessLoginAsync(List<Player> allPlayers)
        {
            string nick = AnsiConsole.Ask<string>("[green]Enter nickname (or '0' to go back): [/] ");

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

                return await ProcessRegisterAsync(allPlayers, nick);
            }
        }

        private static Player ProcessPassword(Player existingPlayer)
        {
            while (true)
            {
                string password = AnsiConsole.Prompt(
                    new TextPrompt<string>("[blue]Enter password (or '0' to go back): [/] ")
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

                AnsiConsole.MarkupLine("[bold red]Incorrect password[/]");
            }
        }

        private static async Task<Player> ProcessRegisterAsync(List<Player> allPlayers, string defaultNick = null)
        {
            string nick = defaultNick;

            if (string.IsNullOrWhiteSpace(nick))
            {
                nick = AnsiConsole.Ask<string>("[green]Create a nickname (or '0' to go back): [/] ");
            }

            if (nick == "0")
            {
                return null;
            }

            while (allPlayers.Any(p => p.NickName == nick))
            {
                AnsiConsole.MarkupLine("\n[bold red]This nickname is already taken[/]");
                nick = AnsiConsole.Ask<string>("[green]Choose another nickname (or '0' to go back): [/] ");

                if (nick == "0")
                {
                    return null;
                }
            }

            string password = AnsiConsole.Prompt(
                new TextPrompt<string>("[blue]Create a password (or '0' to go back): [/] ")
                    .Secret());

            if (password == "0")
            {
                return null;
            }

            return await CreateNewPlayerAsync(allPlayers, nick, password);
        }

        private static async Task<Player> CreateNewPlayerAsync(List<Player> players, string nick, string pass)
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

            await SaveProfilesAsync(players);

            AnsiConsole.MarkupLine($"\n[bold green]Profile '{finalNick}' created![/]");

            return newPlayer;
        }
    }
}