using KpiSessionSimulator.Core;
using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Services; 
using KpiSessionSimulator.Teachers;
using Spectre.Console;

namespace KpiSessionSimulator.Minigames
{
    public class Roulette : IMiniGame
    {
        public const int BulletSlots = 6;
        public const int MaxBullets = 4;
        public const int StartNumOfBullets = 2;

        public int CurrBullets { get; private set; }

        public Roulette()
        {
            CurrBullets = StartNumOfBullets;
        }

        public bool Play(Player player, BasicTeacher teacher, int numberOfQuestion)
        {
            AnsiConsole.MarkupLine("\n[grey]You hear the spin of the revolver cylinder[/]");
            AnsiConsole.MarkupLine($"\n[red]{teacher.Name}: Maby this will help if you didn`t study properly...[/]");
            AnsiConsole.MarkupLine($"[darkorange]Bullet count: {CurrBullets}/{BulletSlots}[/]");

            bool hasImmunity = UseImmunity(player);

            AnsiConsole.MarkupLine("\n[grey](Press any key to pull the trigger)[/]");
            Console.ReadKey(true);

            bool isShot = CheckShot();

            if (!isShot)
            {
                AnsiConsole.MarkupLine("\n[bold grey]You hear a metallic 'click'[/]");
                Thread.Sleep(GameSettings.ShortPauseMs);
                AnsiConsole.MarkupLine($"\n[bold green]{teacher.Name}: Today is your second birthday![/]");

                if (CurrBullets < MaxBullets)
                {
                    CurrBullets++;
                    AnsiConsole.MarkupLine($"[darkorange]The number of bullets in the roulette has been increased to {CurrBullets}[/]");
                }

                return true;
            }
            else
            {
                AnsiConsole.MarkupLine("\n[bold red]BANG![/]");
                Thread.Sleep(GameSettings.ShortPauseMs);

                if (hasImmunity)
                {
                    AnsiConsole.MarkupLine("[cyan]The bullet in your head begins to dissolve, and the wound heals[/]");

                    return true;
                }
                else
                {
                    AnsiConsole.MarkupLine($"[bold darkred]An ambulance takes you away! {teacher.Name} quietly hides the revolver in the desk[/]");

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
                bool usePerk = UIHelper.AskYesNo("\n[cyan]Use 'Immunity' perk?[/]");

                if (usePerk)
                {
                    var stats = player.Stats;
                    stats.ImmunityCount--;
                    player.Stats = stats;

                    return true;
                }
            }

            return false;
        }

        private bool CheckShot()
        {
            Random rnd = new Random();

            return rnd.Next(1, BulletSlots + 1) <= CurrBullets;
        }
    }
}
