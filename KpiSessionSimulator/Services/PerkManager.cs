using KpiSessionSimulator.Models;
using Spectre.Console;

namespace KpiSessionSimulator.Services
{
    public class PerkManager
    {
        public Question UseLoyalty(Player player, Func<Question> pullTicketAction)
        {
            if (player.Stats.LoyaltyCount > 0)
            {
                bool usePerk = UIHelper.AskYesNo("\n[cyan]Use 'Loyalty' perk to change the ticket?[/]");

                if (usePerk)
                {
                    AnsiConsole.MarkupLine("\n[cyan]The teacher sees your frightened eyes and lets you pick another ticket...[/]");
                    var stats = player.Stats;
                    stats.LoyaltyCount--;
                    player.Stats = stats;

                    return pullTicketAction(); 
                }
            }

            return null;
        }

        public bool UseEagleEye(Player player, Question question)
        {
            if (player.Stats.EagleEyeCount > 0)
            {
                bool usePerk = UIHelper.AskYesNo("\n[cyan]Use 'Blue Eye' perk?[/]");

                if (usePerk)
                {
                    AnsiConsole.MarkupLine("\n[cyan]You caught a squirrel that stole one wrong answer![/]");
                    var stats = player.Stats;
                    stats.EagleEyeCount--;
                    player.Stats = stats;

                    List<int> wrongIdx = new List<int>();

                    for (int i = 0; i < question.Options.Count; i++)
                    {
                        if (i != question.IndexOfCorrectAnswer && !question.Options[i].Contains("REMOVED"))
                        {
                            wrongIdx.Add(i);
                        }
                    }

                    if (wrongIdx.Count > 0)
                    {
                        Random rnd = new Random();
                        int randomWrongIdx = wrongIdx[rnd.Next(wrongIdx.Count)];

                        question.Options[randomWrongIdx] = "[grey]--- REMOVED ---[/]";
                    }

                    return true;
                }
            }

            return false;
        }
    }
}
