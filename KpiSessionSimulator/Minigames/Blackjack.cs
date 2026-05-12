using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;
using KpiSessionSimulator.Services; 
using Spectre.Console;

namespace KpiSessionSimulator.Minigames
{
    public class Blackjack : IMiniGame
    {
        private Random Rnd = new Random();
        private const int ShortPauseMs = 1500;

        public bool Play(Player player, BasicTeacher teacher, int numberOfQuestion)
        {
            int scorePlayer = Rnd.Next(1, 11);
            int scoreTeacher = Rnd.Next(1, 11);

            AnsiConsole.MarkupLine($"\n[green]{player.NickName}'s score:[/] [bold]{scorePlayer}[/]");
            AnsiConsole.MarkupLine($"[red]{teacher.Name}'s score:[/] [bold]{scoreTeacher}[/]");

            scorePlayer = PlayerTurn(player, scorePlayer);

            if (scorePlayer > 21) return false;

            AnsiConsole.MarkupLine($"\n[red]{teacher.Name} is drawing cards...[/]");
            Thread.Sleep(ShortPauseMs);

            scoreTeacher = TeacherTurn(teacher, scoreTeacher);

            if (scoreTeacher > 21) return true;

            return SelectWinner(player, teacher, scorePlayer, scoreTeacher);
        }

        private int PlayerTurn(Player player, int curScore)
        {
            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\n[yellow]Choose your action:[/]")
                        .AddChoices(new[] { "Hit", "Stand" }));

                if (choice == "Hit")
                {
                    int newCard = Rnd.Next(1, 11);
                    curScore += newCard;

                    AnsiConsole.MarkupLine($"\n[grey]You draw a card: {newCard}[/]");
                    AnsiConsole.MarkupLine($"[green]{player.NickName}'s score:[/] [bold]{curScore}[/]");

                    if (curScore > 21)
                    {
                        if (player.Stats.TrickyHandsCount > 0 && UseTrickyHands(player, ref curScore, newCard))
                        {
                            continue;
                        }

                        AnsiConsole.MarkupLine("\n[bold red]BUST! You exceeded 21[/]");
                        return curScore;
                    }
                }
                else if (choice == "Stand")
                {
                    AnsiConsole.MarkupLine($"\n[grey]You stand with a score of {curScore}[/]");
                    return curScore;
                }
            }
        }

        private int TeacherTurn(BasicTeacher teacher, int curScore)
        {
            while (curScore < 17)
            {
                Thread.Sleep(ShortPauseMs);
                int newCard = Rnd.Next(1, 11);
                curScore += newCard;

                AnsiConsole.MarkupLine($"[grey]{teacher.Name} draws a card: {newCard}[/]");
                AnsiConsole.MarkupLine($"[red]{teacher.Name}'s score:[/] [bold]{curScore}[/]");

                if (curScore > 21)
                {
                    AnsiConsole.MarkupLine($"\n[bold green]{teacher.Name} BUSTED! You win![/]");
                    return curScore;
                }
            }

            AnsiConsole.MarkupLine($"\n[grey]{teacher.Name} stands with a score of {curScore}[/]");
            return curScore;
        }

        private bool SelectWinner(Player player, BasicTeacher teacher, int scorePlayer, int scoreTeacher)
        {
            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Rule("[yellow]FINAL RESULTS[/]").RuleStyle("yellow"));

            AnsiConsole.MarkupLine($"[green]{player.NickName}:[/] {scorePlayer}");
            AnsiConsole.MarkupLine($"[red]{teacher.Name}:[/] {scoreTeacher}");

            if (scorePlayer > scoreTeacher)
            {
                AnsiConsole.MarkupLine("\n[bold green]You win the hand![/]");
                return true;
            }
            else
            {
                AnsiConsole.MarkupLine("\n[bold red]The teacher wins. Your grade is lowered![/]");
                return false;
            }
        }

        private bool UseTrickyHands(Player player, ref int score, int lastCard)
        {
            bool usePerk = UIHelper.AskYesNo("\n[cyan]Use 'Tricky Hands' perk to drop the last card?[/]");

            if (usePerk)
            {
                player.Stats.TrickyHandsCount--;
                score -= lastCard;

                AnsiConsole.MarkupLine($"\n[cyan]You sneakily drop the card ({lastCard}) off the table...[/]");
                AnsiConsole.MarkupLine($"[green]{player.NickName}'s score:[/] [bold]{score}[/]");
                return true;
            }
            return false;
        }
    }
}