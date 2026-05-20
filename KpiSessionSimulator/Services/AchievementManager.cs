using Spectre.Console;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Core
{
    public static class AchievementManager
    {
        private static void GiveAchievement(Player player, string achievementName)
        {
            var stats = player.Stats;

            if (stats.Achievements == null)
            {
                stats.Achievements = new List<string>();
            }

            if (!stats.Achievements.Contains(achievementName))
            {
                stats.Achievements.Add(achievementName);
                player.Stats = stats;

                AnsiConsole.MarkupLine($"\n[bold gold1]Achievement has been unlocked: {achievementName}![/]");
            }
        }

        public static void CheckAchievementsAfterExam(Player player, BasicTeacher teacher)
        {
            if (player.Stats.PassedExams == 1)
            {
                GiveAchievement(player, "First Blood");
            }
            else if (player.Stats.PassedExams == 10)
            {
                GiveAchievement(player, "Veteran");
            }
            else if (player.Stats.PassedExams == 30)
            {
                GiveAchievement(player, "Legend of KPI");
            }

            if (teacher is MatanTeacher && player.Stats.IsONSecondary)
            {
                GiveAchievement(player, "Matan Survivor");
            }
        }

        public static void CheckRouletteAchievements(Player player)
        {
            GiveAchievement(player, "Terminator");
        }

        public static void DisplayAchievements(Player player)
        {
            AnsiConsole.WriteLine();

            var table = new Table()
                .Border(TableBorder.Rounded) 
                .Title("[bold gold1]Achievements[/]");

            table.AddColumn(new TableColumn("[gold1]Name[/]").Centered());

            if (player.Stats.Achievements == null || player.Stats.Achievements.Count == 0)
            {
                table.AddRow("[grey]None[/]");
            }
            else
            {
                foreach (var ach in player.Stats.Achievements)
                {
                    table.AddRow($"[cyan]{ach}[/]");
                }
            }

            AnsiConsole.Write(table);
        }
    }
}