using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;
using KpiSessionSimulator.Interfaces;
using KpiSessionSimulator.Minigames;
using KpiSessionSimulator.Services; 
using Spectre.Console;

namespace KpiSessionSimulator.Core
{
    public class ProgramProcess
    {
        public const int QuestionsToAnswer = 8;
        public const int QuestionsToPass = 6;

        public const int RouletteProbability = 35;
        public const int BlackjackLossesForPenalty = 3;

        private const int ShortPauseMs = 1500;
        private const int LongPauseMs = 3000;

        private Player Player;
        private BasicTeacher Teacher;
        private List<Question> Questions;
        private Random Rnd = new Random();
        private Roulette ExamRoulette;
        private ExamState State;

        public ProgramProcess(Player player, BasicTeacher teacher, List<Question> questions)
        {
            Player = player;
            Teacher = teacher;
            Questions = questions;
            ExamRoulette = new Roulette();
            State = new ExamState();
        }

        public void Exam()
        {
            Teacher.Interact(Player, State);

            if (Player.Stats.IsExpelled)
            {
                return;
            }

            if (Player.Stats.IsONSecondary)
            {
                State.CurrentDifficulty = Difficulty.Medium;
            }

            AnsiConsole.MarkupLine($"\n[grey]Tickets are on the table. You need to answer {QuestionsToPass}/{QuestionsToAnswer} questions[/]");
            Thread.Sleep(LongPauseMs);

            for (int i = 1; i <= QuestionsToAnswer; i++)
            {
                AnsiConsole.MarkupLine($"\n[yellow]Question {i} of {QuestionsToAnswer}[/] [grey](Difficulty: {State.CurrentDifficulty})[/]");

                Question questionToAnswer = PullTheTicket();
                bool repeatQuestion = AskQuestion(questionToAnswer, i);

                if (State.IsHospitalized)
                {
                    AnsiConsole.MarkupLine($"\n[bold red]{Teacher.Name}: My dear student, you have a hole in your head. I think the exam is over for you...[/]");
                    break;
                }

                if (repeatQuestion)
                {
                    i--;
                    AnsiConsole.MarkupLine("\n[cyan]You get new tickets because you won a second chance...[/]");
                    Thread.Sleep(ShortPauseMs);
                }
            }

            if (!Player.Stats.IsExpelled && !State.IsHospitalized)
            {
                AnsiConsole.MarkupLine($"\n[bold]Exam Finished...[/]");
                AnsiConsole.MarkupLine($"Your result: [yellow]{State.CorrectAnswers} out of {QuestionsToAnswer}[/] correct answers");

                if (State.CorrectAnswers >= QuestionsToPass)
                {
                    AnsiConsole.MarkupLine("[bold green]Congratulations! You passed the exam![/]");
                    Player.Stats.Tokens += 150;
                    Player.Stats.IsONSecondary = false;
                    Player.Stats.PassedExams++;
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]You didn't reach the passing score...[/]");
                    Teacher.Punish(Player);
                }
            }
        }

        private Question UseLoyalty()
        {
            if (Player.Stats.LoyaltyCount > 0)
            {
                bool usePerk = UIHelper.AskYesNo("\n[cyan]Use 'Loyalty' perk to change the ticket?[/]");

                if (usePerk)
                {
                    AnsiConsole.MarkupLine("\n[cyan]The teacher sees your frightened eyes and lets you pick another ticket...[/]");
                    Player.Stats.LoyaltyCount--;
                    return PullTheTicket();
                }
            }
            return null;
        }

        private bool UseEagleEye(Question question)
        {
            if (Player.Stats.EagleEyeCount > 0)
            {
                bool usePerk = UIHelper.AskYesNo("\n[cyan]Use 'Blue Eye' perk?[/]");

                if (usePerk)
                {
                    AnsiConsole.MarkupLine("\n[cyan]You caught a squirrel that stole one wrong answer![/]");
                    Player.Stats.EagleEyeCount--;

                    List<int> wrongIdx = new List<int>();
                    for (int i = 0; i < question.Options.Count; i++)
                    {
                        if (i != question.IndexOfCorrectAnswer && question.Options[i] != "[REMOVED]")
                        {
                            wrongIdx.Add(i);
                        }
                    }

                    if (wrongIdx.Count > 0)
                    {
                        int randomWrongIdx = wrongIdx[Rnd.Next(wrongIdx.Count)];
                        question.Options[randomWrongIdx] = "[REMOVED]";
                    }
                    return true;
                }
            }
            return false;
        }

        private Question PullTheTicket()
        {
            int t1 = Rnd.Next(1, 10);
            int t2 = Rnd.Next(10, 20);
            int t3 = Rnd.Next(20, 31);

            Question q1 = GetQuestion();
            Question q2 = GetQuestion();
            Question q3 = GetQuestion();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Choose a ticket:[/]")
                    .AddChoices(new[] { $"Ticket N.{t1}", $"Ticket N.{t2}", $"Ticket N.{t3}" }));

            if (choice.Contains(t1.ToString())) return q1;
            if (choice.Contains(t2.ToString())) return q2;
            return q3;
        }

        private bool AskQuestion(Question question, int questionNum)
        {
            var panel = new Panel($"[bold]{question.Text}[/]")
                .BorderColor(Color.Yellow)
                .Padding(1, 1);
            AnsiConsole.Write(panel);

            Question newQuestion = UseLoyalty();
            if (newQuestion != null) return AskQuestion(newQuestion, questionNum);

            UseEagleEye(question);

            var displayOptions = new List<string>();
            for (int i = 0; i < question.Options.Count; i++)
            {
                displayOptions.Add($"{i + 1}. {question.Options[i]}");
            }

            var answerStr = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Select your answer:[/]")
                    .AddChoices(displayOptions));

            int answerIdx = int.Parse(answerStr.Substring(0, 1)) - 1;

            if (answerIdx == question.IndexOfCorrectAnswer)
            {
                AnsiConsole.MarkupLine("\n[bold green]Correct answer![/]");
                State.CorrectAnswers++;
                Player.WrongAnswersStreak = 0;
                return false;
            }
            else
            {
                return HandleWrongAnswer(questionNum);
            }
        }

        private bool HandleWrongAnswer(int questionNum)
        {
            AnsiConsole.MarkupLine($"\n[bold red]Wrong! {Teacher.Name} burns a hole in you with their eyes...[/]");
            Player.WrongAnswersStreak++;

            bool playMinigame = UIHelper.AskYesNo("\n[red]Play a minigame to survive?[/]");

            if (playMinigame)
            {
                bool isRoulette = Rnd.Next(1, 101) <= RouletteProbability;
                IMiniGame miniGame = isRoulette ? ExamRoulette : new Blackjack();

                AnsiConsole.MarkupLine("\n[grey]Preparing for the minigame...[/]");
                Thread.Sleep(ShortPauseMs);

                AnsiConsole.Write(new Rule(isRoulette ? "[red]MINIGAME: ROULETTE[/]" : "[red]MINIGAME: BLACKJACK[/]").RuleStyle("red"));
                Thread.Sleep(ShortPauseMs);

                bool wonMinigame = miniGame.Play(Player, Teacher, questionNum);

                Thread.Sleep(ShortPauseMs);
                AnsiConsole.Write(new Rule("[grey]Returning to the exam...[/]").RuleStyle("grey"));
                Thread.Sleep(ShortPauseMs);

                if (wonMinigame)
                {
                    AnsiConsole.MarkupLine("[bold green]You won the minigame! You get a second chance[/]");
                    return true;
                }
                else
                {
                    AnsiConsole.MarkupLine("[bold red]You lost the minigame! The teacher starts punishing you...[/]");

                    if (!isRoulette)
                    {
                        State.BlackjackLosses++;
                        if (State.BlackjackLosses % BlackjackLossesForPenalty == 0)
                        {
                            AnsiConsole.MarkupLine($"[red]You lost Blackjack {State.BlackjackLosses} times[/]");
                            IncreaseDifficulty();
                        }
                    }
                    else
                    {
                        State.IsHospitalized = true;
                    }
                }
            }
            else
            {
                AnsiConsole.MarkupLine("\n[grey]You took the minus without a fight[/]");
                IncreaseDifficulty();
            }
            return false;
        }

        private void IncreaseDifficulty()
        {
            switch (State.CurrentDifficulty)
            {
                case Difficulty.Easy:
                    State.CurrentDifficulty = Difficulty.Normal;
                    AnsiConsole.MarkupLine("\n[yellow]Difficulty increased to 'Normal'[/]");
                    break;
                case Difficulty.Normal:
                    State.CurrentDifficulty = Difficulty.Medium;
                    AnsiConsole.MarkupLine("\n[darkorange]The teacher asks additional questions. Difficulty is 'Medium'[/]");
                    break;
                case Difficulty.Medium:
                    State.CurrentDifficulty = Difficulty.Difficult;
                    AnsiConsole.MarkupLine("\n[red]The teacher frowns. The next questions will be very difficult![/]");
                    break;
                case Difficulty.Difficult:
                    State.CurrentDifficulty = Difficulty.DeathMode;
                    AnsiConsole.MarkupLine("\n[bold red]DEATH MODE! Pray to God.[/]");
                    break;
                case Difficulty.DeathMode:
                    AnsiConsole.MarkupLine("\n[bold darkred]Difficulty is maxed out. Dopka is imminent...[/]");
                    break;
            }
        }

        private Question GetQuestion()
        {
            var availableQuestions = Questions.Where(q => q.Difficulty == State.CurrentDifficulty).ToList();
            if (availableQuestions.Count == 0) return Questions[Rnd.Next(Questions.Count)];
            int idx = Rnd.Next(availableQuestions.Count);
            return availableQuestions[idx];
        }
    }
}