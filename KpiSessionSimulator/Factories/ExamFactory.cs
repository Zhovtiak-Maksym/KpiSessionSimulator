using KpiSessionSimulator.Core;
using KpiSessionSimulator.Models;
using KpiSessionSimulator.Teachers;

namespace KpiSessionSimulator.Factories
{
    public class ExamFactory
    {
        private const string OpQuestionsFilePath = "Data/op_questions.json";
        private const string AsdQuestionsFilePath = "Data/asd_questions.json";
        private const string MatanQuestionsFilePath = "Data/matan_questions.json";

        public static ExamData GetExamSetUp(string choice)
        {
            BasicTeacher teacher;
            List<Question> questions;

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\nЕкзамен з ОП...");
                    teacher = new OpTeacher();
                    questions = Services.QuestionsLoader.LoadQuestions(OpQuestionsFilePath);
                    break;
                case "2":
                    Console.WriteLine("\nЕкзамен з АСД...");
                    teacher = new ASDTeacher();
                    questions = Services.QuestionsLoader.LoadQuestions(AsdQuestionsFilePath);
                    break;
                case "3":
                    Console.WriteLine("\nЕкзамен з Матану...");
                    teacher = new MatanTeacher();
                    questions = Services.QuestionsLoader.LoadQuestions(MatanQuestionsFilePath);
                    break;
                default:
                    Console.WriteLine("\nЕкзамену з такою опцією немає! Ви відправляєтесь до Пана Легези");
                    goto case "3";
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
