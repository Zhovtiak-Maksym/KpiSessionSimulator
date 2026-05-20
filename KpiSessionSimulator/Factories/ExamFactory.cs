using KpiSessionSimulator.Core;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Services;
using KpiSessionSimulator.Teachers;
using Spectre.Console;

namespace KpiSessionSimulator.Factories
{
    public class ExamFactory
    {
        public static async Task<ExamData> GetExamSetUpAsync(string choice)
        {
            BasicTeacher teacher;
            List<Question> questions;

            switch (choice)
            {
                case "OP (Skostariev)":
                    AnsiConsole.MarkupLine("\n[bold yellow]Starting OP exam...[/]");
                    teacher = new OpTeacher();
                    questions = await QuestionsLoader.LoadQuestionsAsync(PathsMacker.OpQuestions);
                    break;
                case "ASD (Sulema)":
                    AnsiConsole.MarkupLine("\n[bold yellow]Starting ASD exam...[/]");
                    teacher = new ASDTeacher();
                    questions = await QuestionsLoader.LoadQuestionsAsync(PathsMacker.AsdQuestions);
                    break;
                case "Matan (Leheza)":
                    AnsiConsole.MarkupLine("\n[bold yellow]Starting Matan exam...[/]");
                    teacher = new MatanTeacher();
                    questions = await QuestionsLoader.LoadQuestionsAsync(PathsMacker.MatanQuestions);
                    break;
                default:
                    AnsiConsole.MarkupLine("\n[bold red]No such option! You are sent to Mr.Leheza...[/]");
                    teacher = new MatanTeacher();
                    questions = await QuestionsLoader.LoadQuestionsAsync(PathsMacker.MatanQuestions);
                    break;
            }

            ExamData exData = new ExamData
            {
                Teacher = teacher,
                Questions = questions
            };

            return exData;
        }
    }
}