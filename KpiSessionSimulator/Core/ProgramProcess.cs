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

        private PerkManager PerkHandler;
        private DifficultyManager DiffManager;

        public ProgramProcess(Player player, BasicTeacher teacher, List<Question> questions)
        {
            Player = player;
            Teacher = teacher;
            Questions = questions;
            ExamRoulette = new Roulette();
            State = new ExamState();

            PerkHandler = new PerkManager();
            DiffManager = new DifficultyManager();
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

            FinishExam();
        }

        private void FinishExam()
        {
            if (Player.Stats.IsExpelled || State.IsHospitalized)
            {
                return;
            }

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

            if (choice.Contains(t1.ToString()))
            {
                return q1;
            }
            if (choice.Contains(t2.ToString()))
            {
                return q2;
            }

            return q3;
        }

        private Question GetQuestion()
        {
            var availableQuestions = Questions.Where(q => q.Difficulty == State.CurrentDifficulty).ToList();

            if (availableQuestions.Count == 0)
            {
                return Questions[Rnd.Next(Questions.Count)];
            }

            return availableQuestions[Rnd.Next(availableQuestions.Count)];
        }

        private bool AskQuestion(Question question, int questionNum)
        {
            var panel = new Panel($"[bold]{question.Text}[/]").BorderColor(Color.Yellow).Padding(1, 1);
            AnsiConsole.Write(panel);

            Question newQuestion = PerkHandler.UseLoyalty(Player, PullTheTicket);

            if (newQuestion != null)
            {
                return AskQuestion(newQuestion, questionNum);
            }

            PerkHandler.UseEagleEye(Player, question);

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

            return HandleWrongAnswer(questionNum);
        }

        private bool HandleWrongAnswer(int questionNum)
        {
            AnsiConsole.MarkupLine($"\n[bold red]Wrong! {Teacher.Name} burns a hole in you with their eyes...[/]");
            Player.WrongAnswersStreak++;

            if (!UIHelper.AskYesNo("\n[red]Play a minigame to survive?[/]"))
            {
                AnsiConsole.MarkupLine("\n[grey]You took the minus without a fight[/]");
                DiffManager.IncreaseDifficulty(State);

                return false;
            }

            return PlayMinigame(questionNum);
        }

        private bool PlayMinigame(int questionNum)
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

            ApplyMinigameLoss(isRoulette);

            return false;
        }

        private void ApplyMinigameLoss(bool isRoulette)
        {
            AnsiConsole.MarkupLine("[bold red]You lost the minigame! The teacher starts punishing you...[/]");

            if (isRoulette)
            {
                State.IsHospitalized = true;
            }
            else
            {
                State.BlackjackLosses++;
                if (State.BlackjackLosses % BlackjackLossesForPenalty == 0)
                {
                    AnsiConsole.MarkupLine($"[red]You lost Blackjack {State.BlackjackLosses} times[/]");
                    DiffManager.IncreaseDifficulty(State);
                }
            }
        }
    }
}