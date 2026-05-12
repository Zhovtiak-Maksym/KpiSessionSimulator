using KpiSessionSimulator.Core;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Services;
using KpiSessionSimulator.Teachers;
using Spectre.Console;

namespace KpiSessionSimulator.Factories
{
    public class ExamFactory
    {
        public static ExamData GetExamSetUp(string choice)
        {
            BasicTeacher teacher;
            List<Question> questions;

            switch (choice)
            {
                case "OP (Skostariev)":
                    AnsiConsole.MarkupLine("\n[bold yellow]Starting OP exam...[/]");
                    teacher = new OpTeacher();
                    questions = Services.QuestionsLoader.LoadQuestions(PathsMacker.OpQuestions);
                    break;
                case "ASD (Sulema)":
                    AnsiConsole.MarkupLine("\n[bold yellow]Starting ASD exam...[/]");
                    teacher = new ASDTeacher();
                    questions = Services.QuestionsLoader.LoadQuestions(PathsMacker.AsdQuestions);
                    break;
                case "Matan (Leheza)":
                    AnsiConsole.MarkupLine("\n[bold yellow]Starting Matan exam...[/]");
                    teacher = new MatanTeacher();
                    questions = Services.QuestionsLoader.LoadQuestions(PathsMacker.MatanQuestions);
                    break;
                default:
                    AnsiConsole.MarkupLine("\n[bold red]No such option! You are sent to Mr.Leheza...[/]");
                    goto case "Matan (Leheza)";
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